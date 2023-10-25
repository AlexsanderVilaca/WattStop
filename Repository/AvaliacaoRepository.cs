using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using DnsClient;
using MongoDB.Driver;

namespace APIClient.Repository
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly DataContext _context;
        private readonly AvaliacaoDALNoSQL _DAL;
        private readonly IMapper _mapper;
        public AvaliacaoRepository(DataContext context,IMapper mapper, AvaliacaoDALNoSQL dal)
        {
            _context = context;
            _mapper = mapper;
            _DAL = dal;
        }
        public bool CreateAvaliacao(AvaliacaoModel model)
        {
            try
            {
                _context.Avaliacao.Add(model);
                if (Save())
                {
                    var avaliacaoMap = _mapper.Map<AvaliacaoDTCNoSQL>(model);
                    _DAL.Insert(avaliacaoMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public bool UpdateAvaliacao(AvaliacaoModel model)
        {
            try
            {
                var avaliacao = GetAvaliacao(model.Id);

                avaliacao.Avaliacao = model.Avaliacao;
                avaliacao.PontoRecargaId = model.PontoRecargaId;
                avaliacao.Estrelas = model.Estrelas;

                if (Save())
                {
                    var avaliacaoMap = _mapper.Map<AvaliacaoDTCNoSQL>(model);
                    var filtro = Builders<AvaliacaoDTCNoSQL>.Filter.Eq("_id", model.Id);
                    _DAL.Update(filtro, avaliacaoMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }

        public AvaliacaoModel GetAvaliacao(Guid id)
        {
            return _context.Avaliacao.FirstOrDefault(x => x.Id == id);
        }

        public List<AvaliacaoModel> GetAvaliacoes()
        {
            return _context.Avaliacao.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteAvaliacao(Guid id)
        {
            try
            {
                var avaliacao = GetAvaliacao(id);
                _context.Avaliacao.Remove(avaliacao);
                if (Save())
                {
                    _DAL.Delete(id);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }

        }

        public bool AvaliacaoExists(Guid id)
        {
            return _context.Avaliacao.FirstOrDefault(x=>x.Id == id) != null;
        }
    }
}
