namespace APIClient.DTO
{
    public class EmpresaDTO
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public DateTime DataInclusao { get; set; }
    }
}
