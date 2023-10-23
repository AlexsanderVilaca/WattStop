using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class EmpresaDTCNoSQL
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement]
        public string NomeFantasia { get; set; }
        [BsonElement]
        public string CNPJ { get; set; }
        [BsonElement]
        public string Email { get; set; }

    }
}
