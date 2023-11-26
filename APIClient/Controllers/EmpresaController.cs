using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet, Authorize]
        public IActionResult GetEmpresas()
        {
            var empresas = _mapper.Map<List<EmpresaDTO>>(_empresaRepository.GetEmpresas());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresas);
        }

        [HttpGet, Authorize]
        public IActionResult GetEmpresaByCnpj(string cnpj)
        {
            if (!_empresaRepository.EmpresaExists(cnpj))
                return NotFound();
            var empresa = _mapper.Map<EmpresaDTO>(_empresaRepository.GetEmpresa(cnpj));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }
        [HttpGet, Authorize]
        public IActionResult GetEmpresaById(Guid empresaId)
        {
            if (!_empresaRepository.EmpresaExists(empresaId))
                return NotFound();
            var empresa = _mapper.Map<EmpresaDTO>(_empresaRepository.GetEmpresa(empresaId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }
        [HttpGet, Authorize]
        public IActionResult GetEmpresasByName(string name)
        {
            if (!_empresaRepository.SearchEmpresasByName(name))
                return NotFound();
            var empresa = _mapper.Map<List<EmpresaDTO>>(_empresaRepository.GetEmpresasByName(name));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(empresa);
        }

        [HttpPost, Authorize]
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
            if (empresaCreate.UsuarioId == Guid.Empty)
                ModelState.AddModelError("","A empresa precisa ter um usuário atrelado a ela");

            if (_empresaRepository.GetEmpresa(empresaCreate.CNPJ) != null)
                ModelState.AddModelError("", "Já existe uma empresa com este CNPJ");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_empresaRepository.CreateEmpresa(empresaCreate))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut, Authorize]
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
            if (new ValidationHelper().ValidarEmail(empresaUpdate.Email, out msg) == false)
                ModelState.AddModelError("", msg);
            var empresa = _empresaRepository.GetEmpresa(empresaUpdate.CNPJ);

            if (empresa != null && empresa.Id != empresaUpdate.Id)
                ModelState.AddModelError("", "Já existe uma empresa com este CNPJ");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_empresaRepository.UpdateEmpresa(empresaUpdate))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpDelete, Authorize]
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
