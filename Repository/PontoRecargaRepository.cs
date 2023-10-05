using APIClient.Data;
using APIClient.DTO;
using APIClient.Interfaces;
using APIClient.Models;

namespace APIClient.Repository
{
    public class PontoRecargaRepository : IPontoRecargaRepository
    {
        private readonly DataContext _context;
        public PontoRecargaRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePontoRecarga(PontoRecargaModel pontoRecarga)
        {
            _context.PontoRecarga.Add(pontoRecarga);
            return Save();
        }

        public PontoRecargaModel GetPontoRecarga(Guid id)
        {
            return _context.PontoRecarga.FirstOrDefault(x => x.Id == id);
        }

        public List<PontoRecargaModel> GetPontosRecarga()
        {
            return _context.PontoRecarga.OrderBy(p=>p.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
