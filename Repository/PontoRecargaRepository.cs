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

        public bool CreatePontoRecarga(PontoRecargaDTO pontoRecarga)
        {
            throw new NotImplementedException();
        }

        public PontoRecargaModel GetPontoRecarga(Guid id)
        {
            return _context.PontoRecarga.FirstOrDefault(x => x.Id == id);
        }

        public List<PontoRecargaModel> GetPontosRecarga()
        {
            return _context.PontoRecarga.OrderBy(p=>p.Id).ToList();
        }
    }
}
