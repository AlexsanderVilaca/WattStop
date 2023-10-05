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
    [Route("api/[controller]")]
    public class HistoricoPontoRecargaController : Controller
    {
        private readonly IHistoricoRepository _historicoRepository;
        private readonly IMapper _mapper;
        public HistoricoPontoRecargaController(IHistoricoRepository historicoRepository, IMapper mapper)
        {
            _historicoRepository = historicoRepository;
            _mapper = mapper;
        }

        [HttpGet("GetHistorico")]
        [ProducesResponseType(200, Type = typeof(List<HistoricoPontoRecargaModel>))]
        public IActionResult GetHistoricoPontosRecarga()
        {
            //var historicoPontosRecarga = _historicoRepository.GetHistoricoPontosRecarga();
            ////var qrCodes = _mapper.Map<List<EmpresaDTO>>(_qrCodeRepository.GetQRCodes());
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost("CreateHistorico")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHistoricoPontoRecarga([FromBody] HistoricoPontoRecargaDTO historicoPontoCreate)
        {

            //ModelState.Clear();
            //if (historicoPontoCreate == null)
            //    return BadRequest(ModelState);

            //if (historicoPontoCreate.Id == null || historicoPontoCreate.Id == Guid.Empty)
            //    historicoPontoCreate.Id = Guid.NewGuid();

            //EmpresaModel empresaMap = _mapper.Map<EmpresaDTO, EmpresaModel>(qrCodeCreate);

            //if (!_empresaRepository.CreateQRCodes(empresaMap))
            //{
            //    ModelState.AddModelError("", "Algo deu errado na hora de salvar");
            //    return StatusCode(500, ModelState);
            //}
            return Ok();
        }
    }
}
