using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmpresaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaRepository _empresaRepository;
        public EmpresaController(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<EmpresaModel>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmpresas()
        {
            var empresas = _mapper.Map<List<EmpresaDTO>>(_empresaRepository.GetEmpresas());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresas);
        }

        [HttpGet]
        public IActionResult GetEmpresaByCnpj(string cnpj)
        {
            if (!_empresaRepository.EmpresaExists(cnpj))
                return NotFound();
            var empresa = _mapper.Map<EmpresaDTO>(_empresaRepository.GetEmpresa(cnpj));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }
        [HttpGet]
        public IActionResult GetEmpresaById(Guid empresaId)
        {
            if (!_empresaRepository.EmpresaExists(empresaId))
                return NotFound();
            var empresa = _mapper.Map<EmpresaDTO>(_empresaRepository.GetEmpresa(empresaId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }
        [HttpGet]
        public IActionResult GetEmpresasByName(string name)
        {
            if (!_empresaRepository.SearchEmpresasByName(name))
                return NotFound();
            var empresa = _mapper.Map<List<EmpresaDTO>>(_empresaRepository.GetEmpresasByName(name));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmpresa([FromBody] EmpresaDTO empresaCreate)
        {
            ModelState.Clear();
            if (empresaCreate == null)
                return BadRequest(ModelState);

            string msg = "";

            if (empresaCreate.Id == null || empresaCreate.Id == Guid.Empty)
                empresaCreate.Id = Guid.NewGuid();

            if (empresaCreate.DataInclusao == DateTime.MinValue || empresaCreate.DataInclusao == DateTime.MaxValue)
                empresaCreate.DataInclusao = DateTime.Now;

            if (new ValidationHelper().ValidarCNPJ(empresaCreate.CNPJ, out msg) == false)
                ModelState.AddModelError("", "Este CNPJ não é valido. " + msg);

            if (_empresaRepository.GetEmpresa(empresaCreate.CNPJ) != null)
                ModelState.AddModelError("", "Já existe uma empresa com este CNPJ");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            EmpresaModel empresaMap = _mapper.Map<EmpresaDTO, EmpresaModel>(empresaCreate);

            if (!_empresaRepository.CreateEmpresa(empresaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateEmpresa([FromBody] EmpresaDTO empresaUpdate)
        {
            ModelState.Clear();
            if (empresaUpdate == null)
                return BadRequest(ModelState);

            string msg = "";

            if (empresaUpdate.Id == null || empresaUpdate.Id == Guid.Empty)
                ModelState.AddModelError("", "Especifique o Id da empresa a ser alterada");

            if (new ValidationHelper().ValidarCNPJ(empresaUpdate.CNPJ, out msg) == false)
                ModelState.AddModelError("", "Este CNPJ não é valido. " + msg);

            var empresa = _empresaRepository.GetEmpresa(empresaUpdate.CNPJ);

            if (empresa != null && empresa.Id != empresaUpdate.Id)
                ModelState.AddModelError("", "Já existe uma empresa com este CNPJ");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            EmpresaModel empresaMap = _mapper.Map<EmpresaDTO, EmpresaModel>(empresaUpdate);

            if (!_empresaRepository.UpdateEmpresa(empresaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteEmpresa(Guid empresaId)
        {
            ModelState.Clear();

            if (empresaId == Guid.Empty)
                ModelState.AddModelError("", "Especifique o Id da empresa a ser deletada");

            if (_empresaRepository.EmpresaExists(empresaId) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_empresaRepository.DeleteEmpresa(empresaId))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
