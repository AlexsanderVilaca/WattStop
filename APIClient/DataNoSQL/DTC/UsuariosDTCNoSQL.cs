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
        public string? NomeUsuario { get; set; }
        [BsonElement]
        public string? User { get; set; }
        [BsonElement]
        public string? Secret { get; set; }
        [BsonElement]
        public DateTime DT_Criacao { get; set; }
        [BsonElement]
        public DateTime? DT_Alteracao{ get; set; }
        [BsonElement]
        public string TP_Acesso { get; set; }
        [BsonElement]
        public bool Ativo { get; set; }
        [BsonElement]
        public EmpresaDTCNoSQL? Empresa { get; set; }
    }
}
