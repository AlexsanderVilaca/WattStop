using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaController(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository= empresaRepository;
            _mapper = mapper;
        }

        [HttpGet("GetEmpresas")]
        [ProducesResponseType(200, Type = typeof(List<EmpresaModel>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmpresas()
        {
            //var empresas = _mapper.Map<List<EmpresaDTO>>(_empresaRepository.GetEmpresas());
           // if (!ModelState.IsValid)
           //     return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost("CreateEmpresa")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmpresa([FromBody] EmpresaDTO empresaCreate)
        {
            ModelState.Clear();
            if (empresaCreate == null)
                return BadRequest(ModelState);

            if (empresaCreate.Id == null || empresaCreate.Id == Guid.Empty)
                empresaCreate.Id = Guid.NewGuid();

            if (_empresaRepository.GetEmpresa(empresaCreate.CNPJ) != null)
                ModelState.AddModelError("","Já existe uma empresa com este CNPJ");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            EmpresaModel empresaMap = _mapper.Map<EmpresaDTO,EmpresaModel>(empresaCreate);

            if (!_empresaRepository.CreateEmpresa(empresaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
