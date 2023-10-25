using APIClient.Models;

namespace APIClient.Interfaces
{
    public interface IEmpresaRepository
    {
        List<EmpresaModel> GetEmpresas();
        EmpresaModel GetEmpresa(Guid id);
        EmpresaModel GetEmpresa(string cnpj);
        bool CreateEmpresa(EmpresaModel empresa);
        bool UpdateEmpresa(EmpresaModel empresa);
        bool Save();
    }
}
