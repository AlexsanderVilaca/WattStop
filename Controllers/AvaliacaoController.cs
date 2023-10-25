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
    public class AvaliacaoController : Controller
    {
        private readonly IAvaliacaoRepository _repository;
        private readonly IMapper _mapper;
        public AvaliacaoController(IMapper mapper, IAvaliacaoRepository repository)
        {
            _repository = repository;
            _mapper= mapper;
        }

        [HttpGet]
        public IActionResult GetAvaliacoes()
        {
            var avaliacoes = _mapper.Map<List<AvaliacaoDTO>>(_repository.GetAvaliacoes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(avaliacoes);
        }
        [HttpPost]
        public IActionResult CreateAvaliacao([FromBody] AvaliacaoDTO avaliacaoCreate)
        {
            ModelState.Clear();
            
            if (avaliacaoCreate.Id == null || avaliacaoCreate.Id == Guid.Empty)
                avaliacaoCreate.Id = Guid.NewGuid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var avaliacaoMap = _mapper.Map<AvaliacaoDTO, AvaliacaoModel>(avaliacaoCreate);

            if (!_repository.CreateAvaliacao(avaliacaoMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateAvaliacao([FromBody] AvaliacaoDTO avaliacaoUpdate)
        {
            ModelState.Clear();

            if (avaliacaoUpdate.Id == null || avaliacaoUpdate.Id == Guid.Empty)
                ModelState.AddModelError("","Especifique o Id da avaliação a ser alterada");

            if (_repository.AvaliacaoExists(avaliacaoUpdate.Id.Value) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var avaliacaoMap = _mapper.Map<AvaliacaoDTO, AvaliacaoModel>(avaliacaoUpdate);

            if (!_repository.UpdateAvaliacao(avaliacaoMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAvaliacao(Guid avaliacaoId)
        {
            ModelState.Clear();

            if (avaliacaoId == Guid.Empty)
                ModelState.AddModelError("", "Especifique o Id da avaliação a ser alterada");

            if(_repository.AvaliacaoExists(avaliacaoId) == false)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_repository.DeleteAvaliacao(avaliacaoId))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
