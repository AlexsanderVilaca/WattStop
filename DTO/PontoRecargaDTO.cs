namespace APIClient.DTO
{
    public class PontoRecargaDTO
    {
        public Guid? Id { get; set; }
        public string TipoCarregador { get; set; }
        public string Localizacao { get; set; }
        public Guid EmpresaCNPJ { get; set; }
    }
}
