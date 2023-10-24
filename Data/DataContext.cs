using APIClient.Models;
using Microsoft.EntityFrameworkCore;

namespace APIClient.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<AvaliacaoModel> Avaliacao { get; set; }
        public DbSet<PontoRecargaModel> PontoRecarga { get; set; }
        public DbSet<HistoricoPontoRecargaModel> HistoricoPontoRecarga { get; set; }
        public DbSet<EmpresaModel> Empresa { get; set; }
        public DbSet<QrCodeModel> QrCode{ get; set; }
        
    }
}
