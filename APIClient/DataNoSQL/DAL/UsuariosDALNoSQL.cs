using DataNoSQL.DTC;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DAL
{
    public class UsuariosDALNoSQL : BaseDalNoSQL<UsuariosDTCNoSQL>
    {
        public UsuariosDALNoSQL() : base("WattStop", EnumCollectionNoSQL.Usuarios.ToString()) { }

        public List<UsuariosDTCNoSQL> read(Guid? id = null, string usuario = null, bool? ativo = null, char? tipoAcesso = null)
        {
            var query = from q in GetCollection().AsQueryable() select q;

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (string.IsNullOrEmpty(usuario) == false)
                query = query.Where(x => x.User.Contains(usuario));

            if (tipoAcesso.HasValue)
                query = query.Where(x => x.TP_Acesso == tipoAcesso);

            if (ativo.HasValue)
                query = query.Where(x => x.Ativo == ativo);

            return query.ToList();

        }
    }
}
