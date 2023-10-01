using APIClient.DTO;
using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IPontoRecargaRepository
    {
        List<PontoRecargaModel> GetPontosRecarga();
        PontoRecargaModel GetPontoRecarga(Guid id);
        bool CreatePontoRecarga(PontoRecargaDTO pontoRecarga);
    }
}
