using DataNoSQL.DTC;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DAL
{
    public class HistoricoPontoRecargaDALNoSQL : BaseDalNoSQL<HistoricoPontoRecargaDTCNoSQL>
    {
        public HistoricoPontoRecargaDALNoSQL() : base("WattStop", EnumCollectionNoSQL.HistoricoPontoRecarga.ToString()) { }

        public List<HistoricoPontoRecargaDTCNoSQL> read(Guid? id = null, Guid? pontoRecargaId = null, bool? disponivel = null)
        {
            var query = from q in GetCollection().AsQueryable() select q;

            if (id.HasValue)
                query = query.Where(x => x.Id == id);

            if (pontoRecargaId.HasValue)
                query = query.Where(x => x.PontoRecargaId == pontoRecargaId);

            if (disponivel.HasValue)
                query = query.Where(x => x.Disponivel==disponivel);

            return query.ToList();

        }
    }
}
