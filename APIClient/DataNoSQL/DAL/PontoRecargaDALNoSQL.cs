using DataNoSQL.DTC;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DAL
{
    public class PontoRecargaDALNoSQL : BaseDalNoSQL<PontoRecargaDTCNoSQL>
    {
        public PontoRecargaDALNoSQL() : base("WattStop", EnumCollectionNoSQL.PontoRecarga.ToString()) { }

        public List<PontoRecargaDTCNoSQL> read(Guid? id = null, Guid? empresaId = null, string tipoCarregador = null, string localizacao = null)
        {
            var query = from q in GetCollection().AsQueryable() select q;

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (string.IsNullOrEmpty(tipoCarregador) == false)
                query = query.Where(x => x.TipoCarregador.Contains(tipoCarregador));

            if (string.IsNullOrEmpty(localizacao) == false)
                query = query.Where(x => x.Localizacao.Contains(localizacao));

            if (empresaId.HasValue)
                query = query.Where(x => x.EmpresaId == empresaId);

            return query.ToList();

        }
    }
}
