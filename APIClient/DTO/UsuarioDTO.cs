namespace APIClient.DTO
{
    public class UsuarioDTO
    {
        public Guid? Id { get; set; }
        public String? User { get; set; }
        public String? Secret { get; set; }
        public DateTime? DT_Criacao { get; set; }
        public DateTime? DT_Alteracao { get; set; }
        public String? TP_Acesso { get; set; }
        public bool? Ativo { get; set; }
    }
}
