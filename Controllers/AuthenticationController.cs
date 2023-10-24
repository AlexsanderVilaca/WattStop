using APIClient.DTO;
using APIClient.Interfaces;
using AutoMapper;
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
        [HttpPost]
        public IActionResult Login([FromBody] UsuarioDTO dto)
        {
            bool _isLoginValid = _usuarioRepository.ValidateUsuario(dto.User, dto.Secret);
            return Ok();
        }

    }
}