using APIClient.DTO;
using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IHistoricoRepository
    {
        List<HistoricoPontoRecargaModel> GetHistoricoPontosRecarga();
        HistoricoPontoRecargaModel GetHistoricoPontoRecarga(Guid id);
        bool CreateHistoricoPontoRecarga(HistoricoPontoRecargaModel historicoPontoRecarga);
        bool HistoricoExists(Guid id);
        bool Save();
    }
}
