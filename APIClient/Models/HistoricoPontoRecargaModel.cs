namespace APIClient.Models
{
    public class HistoricoPontoRecargaModel
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Disponivel { get; set; }
        public Guid PontoRecargaId { get; set; }
    }
}
