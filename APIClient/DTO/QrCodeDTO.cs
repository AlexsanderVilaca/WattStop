namespace APIClient.DTO
{
    public class QrCodeDTO
    {
        public Guid? Id { get; set; }
        public byte[] Conteudo { get; set; }
        public Guid PontoRecargaId { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
