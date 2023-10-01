namespace APIClient.Models
{
    public class QrCodeModel
    {
        public Guid Id { get; set; }
        public byte[] Conteudo { get; set; }
        public Guid PontoRecargaId { get; set; }
    }
}
