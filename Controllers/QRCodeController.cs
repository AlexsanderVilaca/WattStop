using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
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
        public IActionResult GetQrCode(Guid qrCodeId)
        {
            if (!_qrCodeRepository.QRCodeExists(qrCodeId))
                return NotFound();

            var qrCode = _mapper.Map<QrCodeDTO>(_qrCodeRepository.GetQRCode(qrCodeId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(qrCode);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<QrCodeModel>))]
        public IActionResult GetQRCodes()
        {
            var qrCodes = _mapper.Map<List<QrCodeDTO>>(_qrCodeRepository.GetQrCodes());
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

        [HttpPut]
        public IActionResult UpdateQRCode([FromBody] QrCodeDTO qrCodeUpdate)
        {
            
            if (qrCodeUpdate== null)
                return BadRequest(ModelState);
            ModelState.Clear();
            if (qrCodeUpdate.Id == null || qrCodeUpdate.Id== Guid.Empty)
                ModelState.AddModelError("","Especifique o Id do QR Code a ser alterado");

            var qrCodeMap = _mapper.Map<QrCodeDTO, QrCodeModel>(qrCodeUpdate);

            if (!_qrCodeRepository.CreateQRCode(qrCodeMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteQRCode(Guid qrCodeId)
        {
            ModelState.Clear();

            if (qrCodeId == Guid.Empty)
                return BadRequest("Especifique o Id do QR Code a ser alterada");

            if (_qrCodeRepository.QRCodeExists(qrCodeId) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_qrCodeRepository.DeleteQRCode(qrCodeId))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
