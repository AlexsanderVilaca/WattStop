using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IAvaliacaoRepository
    {
        List<AvaliacaoModel> GetAvaliacoes();   
        AvaliacaoModel GetAvaliacao(Guid id);
        bool CreateAvaliacao(AvaliacaoModel model);
        bool Save();
    }
}
