using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClient.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly DataContext _context;
        private readonly IQRCodeRepository _qrCodeRepository;
        public QRCodeController(IQRCodeRepository qrCodeRepository, DataContext context)
        {
            _qrCodeRepository = qrCodeRepository;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PontoRecargaModel>))]
        public IActionResult GetQRCodes()
        {
            //var qrCodes = _qrCodeRepository.GetQRCodes();
            var qrCodes = _mapper.Map<List<EmpresaDTO>>(_qrCodeRepository.GetQRCodes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(qrCodes);
        }

        [HttpPost("CreateQRCode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQRCodes([FromBody] QrCodeDTO qrCodeCreate)
        {

            ModelState.Clear();
            if (pontoCreate == null)
                return BadRequest(ModelState);

            if (qrCodeCreate.Id == null || qrCodeCreate.Id == Guid.Empty)
                qrCodeCreate.Id = Guid.NewGuid();

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
