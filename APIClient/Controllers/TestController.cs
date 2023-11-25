using APIClient.Interfaces;
using APIClient.Models;
using APIClient.Repository;
using DataNoSQL;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIClient.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        private static List<string> Localizacoes;
        private enum TipoCarregador { AC = 0, DC = 1 };

        private readonly IPontoRecargaRepository _pontoRepository;
        private readonly IHistoricoRepository _historicoRepository;

        public TestController(IPontoRecargaRepository pontoRepository, IHistoricoRepository historicoRepository)
        {
            _pontoRepository = pontoRepository;
            _historicoRepository = historicoRepository;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
        [HttpPost, Authorize]
        public IActionResult LoginTeste([FromBody] LoginModel user)
        {
            return Ok();
        }
        [HttpGet, Authorize]
        public IActionResult MockUp()
        {
            var m = new MongoDB<bool>("WattStop");
            m.DropCollection("PontoRecarga");
            m.DropCollection("HistoricoPontoRecarga");
            _pontoRepository.DeletePontosRecarga();
            _historicoRepository.DeleteHistorico();
            Localizacoes = new List<string>();
            Localizacoes.Add("-23.6043094, -46.6818203");
            Localizacoes.Add("-23.5982635, -46.6918105,17");
            Localizacoes.Add("-23.5985184, -46.6813641,17");
            Localizacoes.Add("-23.595735, -46.6728676,17");
            Localizacoes.Add("-23.6025586, -46.6857085,17");

            for (int i = 0; i < 5; i++)
                GeraPontosRecarga(i);

            Console.WriteLine("\nPontos de recarga gerados");
            Thread.Sleep(500);

            for (int i = 0; i < 100; i++)
                GeraHistorico();

            return Ok();
        }

        private void GeraPontosRecarga(int posicaoLocalizacao)
        {
            
            var random = new Random();
            
            var pontoRecargaDalNoSQL = new PontoRecargaDALNoSQL();
            var empresaDal = new EmpresaDALNoSQL();
            var empresas = empresaDal.read();
            

            pontoRecargaDalNoSQL.Insert(new PontoRecargaDTCNoSQL
            {
                Id = Guid.NewGuid(),
                DataInclusao = DateTime.Now,
                Empresa = empresas.ElementAt(random.Next(empresas.Count)),
                Localizacao = Localizacoes.ElementAt(posicaoLocalizacao),
                TipoCarregador = ObterValorAleatorio<TipoCarregador>().ToString()
            });

            var pontoRecarga = pontoRecargaDalNoSQL.read().Last();

            _pontoRepository.CreatePontoRecarga(new PontoRecargaModel
            {
                Id=pontoRecarga.Id,
                DataInclusao=pontoRecarga.DataInclusao,
                EmpresaId=pontoRecarga.Empresa.Id,
                Localizacao=pontoRecarga.Localizacao,
                TipoCarregador=pontoRecarga.TipoCarregador
            });
        }
        private void GeraHistorico()
        {
            var random = new Random();
            var historicoPontoRecargaDal = new HistoricoPontoRecargaDALNoSQL();
            var pontoRecargaDal = new PontoRecargaDALNoSQL();
            var pontosRecarga = pontoRecargaDal.read();
            var listaPontos = new List<Guid>();
            foreach (var pontoRecarga in pontosRecarga)
                listaPontos.Add(pontoRecarga.Id);

            DateTime dataInicio = new DateTime(2023, 1, 1, 0, 0, 0);
            int rangeDays = (DateTime.Today - dataInicio).Days;
            DateTime randomDate = dataInicio.AddDays(random.Next(rangeDays));
            randomDate = randomDate.AddHours(random.Next(25));
            randomDate = randomDate.AddMinutes(random.Next(61));
            randomDate = randomDate.AddSeconds(random.Next(61));
            randomDate = randomDate.AddMilliseconds(random.Next(1000));

            historicoPontoRecargaDal.Insert(new HistoricoPontoRecargaDTCNoSQL
            {
                Id = Guid.NewGuid(),
                DataHora = randomDate,
                Disponivel = random.Next(2) == 0,
                PontoRecargaId = listaPontos.ElementAt(random.Next(listaPontos.Count)),
            });

            var historico = historicoPontoRecargaDal.read().Last();

            _historicoRepository.CreateHistoricoPontoRecarga(new HistoricoPontoRecargaModel
            {
                Id=historico.Id,
                DataHora=historico.DataHora,
                Disponivel=historico.Disponivel,
                PontoRecargaId=historico.PontoRecargaId
            });
        }

        private static T ObterValorAleatorio<T>() where T : Enum
        {
            var valores = Enum.GetValues(typeof(T));
            var r = new Random();
            return (T)valores.GetValue(r.Next(valores.Length));
        }
    }
}
