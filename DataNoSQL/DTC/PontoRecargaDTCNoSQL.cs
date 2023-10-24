using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class PontoRecargaDTCNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string TipoCarregador { get; set; }
        [BsonElement]
        public Guid EmpresaId { get; set; }
        [BsonElement]
        public string Localizacao { get; set; }
        [BsonElement]
        public DateTime DataInclusao { get; set; }
    }
}
