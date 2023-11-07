using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuarioController(IMapper mapper, IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }
        [EnableCors]
        [HttpPost]
        public IActionResult SignUp([FromBody] UsuarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Preencha todos os dados do usuário");
            if (dto.Id.HasValue == false || dto.Id == Guid.Empty)
                dto.Id = Guid.NewGuid();
            if (string.IsNullOrEmpty(dto.User))
                return BadRequest("Preencha o nome de usuário");
            if (string.IsNullOrEmpty(dto.Secret))
                return BadRequest("Preencha a senha");
            if (string.IsNullOrEmpty(dto.TP_Acesso))
                return BadRequest("Preencha o tipo de acesso do usuário");
            if (_usuarioRepository.GetUsuario(dto.User) != null)
                return BadRequest("Este usuário já está cadastrado");

            var user = _mapper.Map<UsuarioModel>(dto);
            if (!_usuarioRepository.CreateUsuario(user))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok(GenerateToken(new LoginModel { User=dto.User,Secret=dto.Secret}));
        }

        [Authorize]
        [HttpPost]
        public IActionResult LoginTeste([FromBody] LoginModel user)
        {
            bool isLoginValid = _usuarioRepository.ValidateUsuario(user.User, user.Secret);

            if (isLoginValid == false)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = GenerateToken(user);

            return Ok(new
            {
                User = user.User,
                Token = token
            });

        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel user)
        {
            try
            {
                bool isLoginValid = _usuarioRepository.ValidateUsuario(user.User, user.Secret);

                if (isLoginValid)
                    return Ok("Login bem-sucedido");
                else
                    return Unauthorized("Login inválido, email ou senha incorretos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UsuarioDTO dto)
        {
            if (dto == null)
                return BadRequest("Preencha todos os dados do usuário");
            if (string.IsNullOrEmpty(dto.User))
                return BadRequest("Preencha o nome de usuário");
            if (string.IsNullOrEmpty(dto.Secret))
                return BadRequest("Preencha a senha");
            if (string.IsNullOrEmpty(dto.TP_Acesso))
                return BadRequest("Preencha o tipo de acesso do usuário");
            if (_usuarioRepository.GetUsuario(dto.User) != null)
                return BadRequest("Este usuário já está cadastrado");

            var user = _mapper.Map<UsuarioModel>(dto);
            if (!_usuarioRepository.UpdateUsuario(user))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUsuarios()
        {
            try
            {
                var usuarios = _mapper.Map<List<UsuarioDTO>>(_usuarioRepository.GetUsuarios());
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult GetUsuario(string user)
        {
            if (!_usuarioRepository.UsuarioExists(user))
                return NotFound();
            var usuario = _mapper.Map<UsuarioDTO>(_usuarioRepository.GetUsuario(user));
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(usuario);
        }

        [HttpDelete]
        public IActionResult DeleteUsuario(string user)
        {
            ModelState.Clear();

            if (string.IsNullOrEmpty(user))
                return BadRequest("Especifique o usuário a ser deletado");

            if (_usuarioRepository.UsuarioExists(user) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_usuarioRepository.Delete(user))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();

        }

        [AllowAnonymous]
        private string GenerateToken(LoginModel user)
        {

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes
            (_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.User),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;

        }
    }
}