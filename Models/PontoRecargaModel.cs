namespace APIClient.Models
{
    public class PontoRecargaModel
    {
        public Guid Id { get; set; }
        public string TipoCarregador { get; set; }
        public Guid EmpresaId { get; set; }
        public string Localizacao { get; set; }
    }
}
