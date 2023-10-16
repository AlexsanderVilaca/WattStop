using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;

namespace APIClient.Repository
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly DataContext _context;
        public AvaliacaoRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateAvaliacao(AvaliacaoModel model)
        {
            _context.Avaliacao.Add(model);
            return Save();
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
    }
}
