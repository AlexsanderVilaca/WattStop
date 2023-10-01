using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PontoRecargaController : Controller
    {
        private readonly DataContext _context;
        private readonly IPontoRecargaRepository _pontoRecargaRepository;
        public PontoRecargaController(IPontoRecargaRepository pontoRecargaRepository, DataContext context) {
            _pontoRecargaRepository = pontoRecargaRepository;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<PontoRecargaModel>))]
        public IActionResult GetPontosRecarga()
        {
            var pontosRecarga = _pontoRecargaRepository.GetPontosRecarga();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pontosRecarga);
        }
    }
}
