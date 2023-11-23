namespace APIClient.Models
{
    public class AvaliacaoModel
    {
        public Guid Id { get; set; }
        public string? Avaliacao { get; set; }
        public decimal Estrelas { get; set; }
        public Guid PontoRecargaId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
