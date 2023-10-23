using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class UsuariosDTCNoSQL
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string Secret { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao{ get; set; }
        public char TipoAcesso { get; set; }
        public bool Ativo { get; set; }
    }
}
