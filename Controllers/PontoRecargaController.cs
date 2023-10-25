using APIClient.Data;
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
    public class PontoRecargaController : Controller
    {
        private readonly IPontoRecargaRepository _pontoRecargaRepository;
        private readonly IMapper _mapper;
        public PontoRecargaController(IPontoRecargaRepository pontoRecargaRepository, IMapper mapper)
        {
            _pontoRecargaRepository = pontoRecargaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PontoRecargaModel>))]
        public IActionResult GetPontosRecarga()
        {
            var pontosRecarga = _mapper.Map<List<PontoRecargaDTO>>(_pontoRecargaRepository.GetPontosRecarga());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pontosRecarga);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePontosRecarga([FromBody] PontoRecargaDTO pontoCreate)
        {
            if (pontoCreate == null)
                return BadRequest(ModelState);

            ModelState.Clear();

            if (pontoCreate.Id == null || pontoCreate.Id == Guid.Empty)
                pontoCreate.Id = Guid.NewGuid();

            var pontoRecargaMap = _mapper.Map<PontoRecargaDTO, PontoRecargaModel>(pontoCreate);

            if (!_pontoRecargaRepository.CreatePontoRecarga(pontoRecargaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePontoRecarga([FromBody] PontoRecargaDTO pontoUpdate)
        {
            if (pontoUpdate== null)
                return BadRequest(ModelState);
            ModelState.Clear();
            if (pontoUpdate.Id == null || pontoUpdate.Id == Guid.Empty)
                return BadRequest("Especifique o Id do ponto de recarga a ser alterado");

            if (_pontoRecargaRepository.PontoRecargaExists(pontoUpdate.Id.Value) == false)
                return NotFound();

            var pontoRecargaMap = _mapper.Map<PontoRecargaDTO, PontoRecargaModel>(pontoUpdate);

            if (!_pontoRecargaRepository.CreatePontoRecarga(pontoRecargaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePontoRecarga(Guid pontoRecargaId)
        {
            ModelState.Clear();

            if (pontoRecargaId == Guid.Empty)
                return BadRequest("Especifique o Id do ponto de recarga a ser deletado");

            if (_pontoRecargaRepository.PontoRecargaExists(pontoRecargaId) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pontoRecargaRepository.DeletePontoRecarga(pontoRecargaId))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
