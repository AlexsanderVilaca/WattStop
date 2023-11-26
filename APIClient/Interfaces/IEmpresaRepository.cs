using APIClient.DTO;
using APIClient.Models;
using DataNoSQL.DTC;

namespace APIClient.Interfaces
{
    public interface IEmpresaRepository
    {
        List<EmpresaDTCNoSQL> GetEmpresas();
        EmpresaModel GetEmpresa(Guid id);
        EmpresaModel GetEmpresa(string cnpj);
        List<EmpresaModel> GetEmpresasByName(string name);
        bool CreateEmpresa(EmpresaDTO empresa);
        bool UpdateEmpresa(EmpresaDTO empresa);
        bool DeleteEmpresa(Guid id);
        bool EmpresaExists(Guid id);
        bool EmpresaExists(string cnpj);
        bool SearchEmpresasByName(string nomeEmpresa);
        bool Save();
    }
}
