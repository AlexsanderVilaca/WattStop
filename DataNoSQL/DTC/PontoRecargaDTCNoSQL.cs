using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class PontoRecargaDTCNoSQL
    {
        public Guid Id { get; set; }
        public string TipoCarregador { get; set; }
        public Guid EmpresaId { get; set; }
        public string Localizacao { get; set; }
    }
}
