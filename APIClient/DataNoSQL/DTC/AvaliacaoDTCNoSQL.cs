using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class AvaliacaoDTCNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string? Avaliacao { get; set; }
        [BsonElement]
        public decimal Estrelas { get; set; }
        [BsonElement]
        public Guid PontoRecargaId { get; set; }
        [BsonElement]
        public Guid UsuarioId { get; set; }
        [BsonElement]
        public string UsuarioNome { get; set; }
        [BsonElement]
        public DateTime DataInclusao { get; set; }
    }
}
