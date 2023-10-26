using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }
        [EnableCors]
        [HttpPost]
        public IActionResult SignUp([FromBody] UsuarioDTO dto)
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
            if (!_usuarioRepository.CreateUsuario(user))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromBody] UsuarioDTO dto)
        {
            bool _isLoginValid = _usuarioRepository.ValidateUsuario(dto.User, dto.Secret);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _mapper.Map<List<UsuarioDTO>>(_usuarioRepository.GetUsuarios());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(usuarios);
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

    }
}