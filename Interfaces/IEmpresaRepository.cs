using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IEmpresaRepository
    {
        List<EmpresaModel> GetEmpresas();
        EmpresaModel GetEmpresa(Guid id);
        EmpresaModel GetEmpresa(string cnpj);
        List<EmpresaModel> GetEmpresasByName(string name);
        bool CreateEmpresa(EmpresaModel empresa);
        bool UpdateEmpresa(EmpresaModel empresa);
        bool DeleteEmpresa(Guid id);
        bool EmpresaExists(Guid id);
        bool EmpresaExists(string cnpj);
        bool SearchEmpresasByName(string nomeEmpresa);
        bool Save();
    }
}
