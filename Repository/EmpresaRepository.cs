using APIClient.Data;
using APIClient.Interfaces;
using APIClient.Models;
using AutoMapper;
using DataNoSQL.DAL;
using DataNoSQL.DTC;
using MongoDB.Driver;

namespace APIClient.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DataContext _context;
        private readonly EmpresaDALNoSQL _DAL;
        private readonly IMapper _mapper;

        public EmpresaRepository(DataContext context, IMapper mapper, EmpresaDALNoSQL dal)
        {
            _context = context;
            _mapper = mapper;
            _DAL = dal;
        }

        public bool CreateEmpresa(EmpresaModel empresa)
        {
            try
            {
                _context.Empresa.Add(empresa);
                if (Save())
                {

                    var empresaMap = _mapper.Map<EmpresaDTCNoSQL>(empresa);
                    _DAL.Insert(empresaMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
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
            var empresasDtc = _DAL.read();
            var empresas = _mapper.Map<List<EmpresaModel>>(empresasDtc);
            return empresas;
        }

        public bool UpdateEmpresa(EmpresaModel empresa)
        {
            try
            {
                _context.Empresa.Update(empresa);
                if (Save())
                {
                    var filtro = Builders<EmpresaDTCNoSQL>.Filter.Eq("_id", empresa.Id);
                    var empresaMap = _mapper.Map<EmpresaDTCNoSQL>(empresa);
                    _DAL.Update(filtro, empresaMap);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return false;
            }
        }
    }
}
