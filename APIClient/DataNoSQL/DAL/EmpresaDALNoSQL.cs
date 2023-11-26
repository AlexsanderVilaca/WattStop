using DataNoSQL.DTC;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DAL
{
    public class EmpresaDALNoSQL:BaseDalNoSQL<EmpresaDTCNoSQL>
    {
        public EmpresaDALNoSQL() : base("WattStop", EnumCollectionNoSQL.Empresa.ToString()) { }

        public List<EmpresaDTCNoSQL> read(Guid? id = null, string cnpj = null, string nomeFantasia = null, string email = null, Guid? usuarioId = null)
        {
            var query = from q in GetCollection().AsQueryable() select q;

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (string.IsNullOrEmpty(cnpj) == false)
                query = query.Where(x => x.CNPJ.Contains(cnpj));

            if (string.IsNullOrEmpty(nomeFantasia) == false)
                query = query.Where(x => x.Nome.Contains(nomeFantasia));
            
            if (string.IsNullOrEmpty(email) == false)
                query = query.Where(x => x.Email.Contains(email));

            if (usuarioId.HasValue)
                query = query.Where(x => x.UsuarioId == usuarioId.Value);

            return query.ToList();

        }
    }
}
