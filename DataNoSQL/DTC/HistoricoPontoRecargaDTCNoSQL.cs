using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class HistoricoPontoRecargaDTCNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public DateTime DataHora { get; set; }
        [BsonElement]
        public bool Disponivel { get; set; }
        [BsonElement]
        public Guid PontoRecargaId { get; set; }
    }
}
