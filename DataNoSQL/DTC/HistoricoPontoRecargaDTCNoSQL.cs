using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL.DTC
{
    public class HistoricoPontoRecargaDTCNoSQL
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; }
        public Guid PontoRecargaId { get; set; }
    }
}
