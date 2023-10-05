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
    public class QRCodeController : Controller
    {
        private readonly DataContext _context;
        private readonly IQRCodeRepository _qrCodeRepository;
        private readonly IMapper _mapper;
        public QRCodeController(IQRCodeRepository qrCodeRepository, DataContext context, IMapper mapper)
        {
            _qrCodeRepository = qrCodeRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<QrCodeModel>))]
        public IActionResult GetQRCodes()
        {
            //var qrCodes = _qrCodeRepository.GetQRCodes();
            ////var qrCodes = _mapper.Map<List<EmpresaDTO>>(_qrCodeRepository.GetQRCodes());
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            return Ok();
        }

        [HttpPost("CreateQRCode")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQRCodes([FromBody] QrCodeDTO qrCodeCreate)
        {

            ModelState.Clear();
            if (qrCodeCreate == null)
                return BadRequest(ModelState);

            if (qrCodeCreate.Id == null || qrCodeCreate.Id == Guid.Empty)
                qrCodeCreate.Id = Guid.NewGuid();

            var empresaMap = _mapper.Map<QrCodeDTO, QrCodeModel>(qrCodeCreate);

            if (!_qrCodeRepository.CreateQRCodes(empresaMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
