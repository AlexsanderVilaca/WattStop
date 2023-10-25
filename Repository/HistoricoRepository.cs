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
        public HistoricoRepository(DataContext context,IMapper mapper, HistoricoPontoRecargaDALNoSQL dal)
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

        public HistoricoPontoRecargaModel GetHistoricoPontoRecarga(Guid id)
        {
            return _context.HistoricoPontoRecarga.FirstOrDefault(x => x.Id == id);
        }

        public List<HistoricoPontoRecargaModel> GetHistoricoPontosRecarga()
        {
            return _context.HistoricoPontoRecarga.OrderByDescending(x => x.DataHora).ToList();
        }

        public bool HistoricoExists(Guid id)
        {
            return _context.HistoricoPontoRecarga.FirstOrDefault(x => x.Id == id) != null;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
