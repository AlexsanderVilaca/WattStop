using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoRecargaController : Controller
    {
        private readonly IPontoRecargaRepository _pontoRecargaRepository;
        private readonly IMapper _mapper;
        public PontoRecargaController(IPontoRecargaRepository pontoRecargaRepository, IMapper mapper) {
            _pontoRecargaRepository = pontoRecargaRepository;
            _mapper = mapper;
        }

        [HttpGet("GetPontosRecarga")]
        [ProducesResponseType(200,Type=typeof(List<PontoRecargaModel>))]
        public IActionResult GetPontosRecarga()
        {
            //var pontosRecarga = _pontoRecargaRepository.GetPontosRecarga();
            ////var pontosRecarga = _mapper.Map<List<EmpresaDTO>>(_pontoRecargaRepository.GetPontosRecarga());
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost("CreatePontoRecarga")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePontosRecarga([FromBody] PontoRecargaDTO pontoCreate) {

            //ModelState.Clear();
            //if (pontoCreate == null)
            //    return BadRequest(ModelState);

            //if (pontoCreate.Id == null || pontoCreate.Id == Guid.Empty)
            //    pontoCreate.Id = Guid.NewGuid();

           // EmpresaModel empresaMap = _mapper.Map<EmpresaDTO, EmpresaModel>(pontoCreate);

            //if (!_empresaRepository.CreatePontosRecarga(empresaMap))
            //{
            //    ModelState.AddModelError("", "Algo deu errado na hora de salvar");
            //    return StatusCode(500, ModelState);
            //}
            return Ok();
        }
    }
}
