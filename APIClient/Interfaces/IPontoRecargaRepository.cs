using APIClient.DTO;
using APIClient.Models;
using DataNoSQL.DTC;

namespace APIClient.Interfaces
{
    public interface IPontoRecargaRepository
    {
        List<PontoRecargaDTCNoSQL> GetPontosRecarga();
        PontoRecargaModel GetPontoRecarga(Guid id);
        List<PontoRecargaModel> GetPontosRecargaByEmpresa(Guid empresaId);
        bool CreatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool UpdatePontoRecarga(PontoRecargaModel pontoRecarga);
        bool DeletePontoRecarga(Guid id);
        bool DeletePontosRecarga();
        bool PontoRecargaExists(Guid id);
        bool PontoRecargaEmpresaExists(Guid empresaId);
        bool Save();
    }
}
