using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;

namespace APIClient.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EmpresaRepository(DataContext context, IMapper mapper)
        {
            _mapper= mapper;
            _context= context;
        }

        public bool CreateEmpresa(EmpresaModel empresa)
        {
            _context.Empresa.Add(empresa);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public EmpresaModel GetEmpresa(Guid id)
        {
            return _context.Empresa.FirstOrDefault(x => x.Id == id);
        }
        public EmpresaModel GetEmpresa(string cnpj)
        {
            return _context.Empresa.FirstOrDefault(x => x.CNPJ == cnpj);
        }

        public List<EmpresaModel> GetEmpresas()
        {
            return _context.Empresa.OrderBy(x => x.NomeFantasia).ToList();
        }

    }
}
