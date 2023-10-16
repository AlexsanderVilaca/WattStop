using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIClient.Repository
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly DataContext _context;

        public HistoricoRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateHistoricoPontoRecarga(HistoricoPontoRecargaModel historicoPontoRecarga)
        {
            _context.HistoricoPontoRecarga.Add(historicoPontoRecarga);
            return Save();
        }

        public HistoricoPontoRecargaModel GetHistoricoPontoRecarga(Guid id)
        {
            return _context.HistoricoPontoRecarga.FirstOrDefault(x => x.Id == id);
        }

        public List<HistoricoPontoRecargaModel> GetHistoricoPontosRecarga()
        {
            return _context.HistoricoPontoRecarga.OrderByDescending(x => x.DataHora).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
