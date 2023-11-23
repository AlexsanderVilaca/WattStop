using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using DnsClient;
using Microsoft.EntityFrameworkCore;

namespace APIClient.Repository
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly HistoricoPontoRecargaDALNoSQL _DAL;
        public HistoricoRepository(DataContext context, IMapper mapper, HistoricoPontoRecargaDALNoSQL dal)
        {
            _context = context;
            _mapper = mapper;
            _DAL = dal;
        }
        public bool CreateHistoricoPontoRecarga(HistoricoPontoRecargaModel historicoPontoRecarga)
        {
            try
            {
                _context.HistoricoPontoRecarga.Add(historicoPontoRecarga);
                if (Save())
                {
                    var historicoMap = _mapper.Map<HistoricoPontoRecargaDTCNoSQL>(historicoPontoRecarga);
                    _DAL.Insert(historicoMap);
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

        public List<HistoricoPontoRecargaModel> GetHistoricoByPontoRecarga(Guid pontoRecargaId)
        {
            var historicoDtc = _DAL.read(pontoRecargaId: pontoRecargaId).OrderByDescending(x => x.DataHora).ToList();
            var Log = _mapper.Map<List<HistoricoPontoRecargaModel>>(historicoDtc);
            return Log;
        }

        public HistoricoPontoRecargaModel GetHistoricoPontoRecarga(Guid id)
        {
            var historicoDtc = _DAL.read(id: id);
            var Log = _mapper.Map<HistoricoPontoRecargaModel>(historicoDtc);
            return Log;
        }

        public List<HistoricoPontoRecargaModel> GetHistoricoPontosRecarga()
        {
            var historicoDtc = _DAL.read().OrderByDescending(x => x.DataHora).ToList();
            var Log = _mapper.Map<List<HistoricoPontoRecargaModel>>(historicoDtc);
            return Log;
        }

        public bool HistoricoExists(Guid id)
        {
            var historicoDtc = _DAL.read(id: id);
            var Log = _mapper.Map<List<HistoricoPontoRecargaModel>>(historicoDtc);
            return Log.FirstOrDefault() == null ? false : true;
        }

        public bool HistoricoPontoExists(Guid pontoRecargaId)
        {
            var historicoDtc = _DAL.read(pontoRecargaId: pontoRecargaId);
            var Log = _mapper.Map<List<HistoricoPontoRecargaModel>>(historicoDtc);
            return Log.FirstOrDefault() == null ? false : true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
