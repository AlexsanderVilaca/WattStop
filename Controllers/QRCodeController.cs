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
    public class QRCodeController : Controller
    {
        private readonly IQRCodeRepository _qrCodeRepository;
        private readonly IMapper _mapper;
        public QRCodeController(IQRCodeRepository qrCodeRepository, IMapper mapper)
        {
            _qrCodeRepository = qrCodeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<QrCodeModel>))]
        public IActionResult GetQRCodes()
        {
            var qrCodes = _mapper.Map<List<EmpresaDTO>>(_qrCodeRepository.GetQrCodes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(qrCodes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQRCode([FromBody] QrCodeDTO qrCodeCreate)
        {

            ModelState.Clear();
            if (qrCodeCreate == null)
                return BadRequest(ModelState);

            if (qrCodeCreate.Id == null || qrCodeCreate.Id == Guid.Empty)
                qrCodeCreate.Id = Guid.NewGuid();

            var qrCodeMap = _mapper.Map<QrCodeDTO, QrCodeModel>(qrCodeCreate);

            if (!_qrCodeRepository.CreateQRCode(qrCodeMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
