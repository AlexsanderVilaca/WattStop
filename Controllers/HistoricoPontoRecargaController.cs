using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HistoricoPontoRecargaController : Controller
    {
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IMapper _mapper;
        public HistoricoPontoRecargaController(IHistoricoRepository historicoRepository, IMapper mapper)
        {
            _historicoRepository = historicoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<HistoricoPontoRecargaModel>))]
        public IActionResult GetHistoricoPontosRecarga()
        {
            var historicoPontosRecarga = _historicoRepository.GetHistoricoPontosRecarga();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(historicoPontosRecarga);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHistoricoPontoRecarga([FromBody] HistoricoPontoRecargaDTO historicoPontoCreate)
        {

            ModelState.Clear();
            if (historicoPontoCreate == null)
                return BadRequest(ModelState);

            if (historicoPontoCreate.Id == null || historicoPontoCreate.Id == Guid.Empty)
                historicoPontoCreate.Id = Guid.NewGuid();

            var historicoMap = _mapper.Map<HistoricoPontoRecargaDTO, HistoricoPontoRecargaModel>(historicoPontoCreate);

            if (!_historicoRepository.CreateHistoricoPontoRecarga(historicoMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
