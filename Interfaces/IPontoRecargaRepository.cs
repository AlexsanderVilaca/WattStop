using APIClient.DTO;
using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IPontoRecargaRepository
    {
        List<PontoRecargaModel> GetPontosRecarga();
        PontoRecargaModel GetPontoRecarga(Guid id);
        List<PontoRecargaModel> GetPontosRecargaByEmpresa(Guid empresaId);
        bool CreatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool UpdatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool DeletePontoRecarga(Guid id);
        bool PontoRecargaExists(Guid id);
        bool PontoRecargaEmpresaExists(Guid empresaId);
        bool Save();
    }
}
