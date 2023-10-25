using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
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

        private bool ValidarCNPJ(string cnpj, out string msg)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            msg = "Este CNPJ é válido";
            if (cnpj.Length != 14)
            {
                msg = "O CNPJ deve possuir 14 dígitos";
                return false;
            }
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string parte1 = cnpj.Substring(0, 12);
            string digitoVerificador = cnpj.Substring(12);

            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(parte1[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;

            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            parte1 = parte1 + digito1;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(parte1[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;

            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digitoVerificador == $"{digito1}{digito2}";
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

            if (ValidarCNPJ(empresaCreate.CNPJ, out msg) == false)
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

            if (ValidarCNPJ(empresaUpdate.CNPJ, out msg) == false)
                ModelState.AddModelError("", "Este CNPJ não é valido. " + msg);

            if (_empresaRepository.GetEmpresa(empresaUpdate.CNPJ) != null && _empresaRepository.GetEmpresa(empresaUpdate.CNPJ).Id != empresaUpdate.Id)
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
