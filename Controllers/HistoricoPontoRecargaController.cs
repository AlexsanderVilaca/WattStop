using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClient.Controllers
{
    public class HistoricoPontoRecargaController : Controller
    {
        private readonly DataContext _context;
        private readonly IHistoricoRepository _historicoRepository;
        public HistoricoPontoRecargaController(IHistoricoRepository historicoRepository, DataContext context)
        {
            _historicoRepository = historicoRepository;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<HistoricoPontoRecargaModel>))]
        public IActionResult GetHistoricoPontosRecarga()
        {
            var historicoPontosRecarga = _historicoRepository.GetHistoricoPontosRecarga();
            //var qrCodes = _mapper.Map<List<EmpresaDTO>>(_qrCodeRepository.GetQRCodes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(historicoPontosRecarga);
        }

        [HttpPost("CreateQRCode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQRCodes([FromBody] HistoricoPontoRecargaDTO historicoPontoCreate)
        {

            ModelState.Clear();
            if (historicoPontoCreate == null)
                return BadRequest(ModelState);

            if (historicoPontoCreate.Id == null || historicoPontoCreate.Id == Guid.Empty)
                historicoPontoCreate.Id = Guid.NewGuid();

            EmpresaModel empresaMap = _mapper.Map<EmpresaDTO, EmpresaModel>(qrCodeCreate);

            if (!_empresaRepository.CreateQRCodes(empresaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
