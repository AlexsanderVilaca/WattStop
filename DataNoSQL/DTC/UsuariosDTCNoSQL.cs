using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class UsuariosDTCNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string? User { get; set; }
        [BsonElement]
        public string? Secret { get; set; }
        [BsonElement]
        public DateTime DataInclusao { get; set; }
        [BsonElement]
        public DateTime? DataAlteracao{ get; set; }
        [BsonElement]
        public char TipoAcesso { get; set; }
        [BsonElement]
        public bool Ativo { get; set; }
    }
}
