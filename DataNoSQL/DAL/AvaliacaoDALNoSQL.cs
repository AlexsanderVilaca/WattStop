using DataNoSQL.DTC;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DAL
{
    public class AvaliacaoDALNoSQL : BaseDalNoSQL<AvaliacaoDTCNoSQL>
    {

        public AvaliacaoDALNoSQL() : base("WattStop", EnumCollectionNoSQL.Avaliacao.ToString()) { }

        public List<AvaliacaoDTCNoSQL> read(Guid? id = null, Guid? pontoRecargaId = null)
        {
            var query = from q in GetCollection().AsQueryable() select q;

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (pontoRecargaId.HasValue)
                query = query.Where(x => x.PontoRecargaId==pontoRecargaId);

            return query.ToList();

        }
    }
}
