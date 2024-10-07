namespace Domain.Entidades.Cadastros.Piloto
{
    public class Piloto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public byte[]? Assinatura { get; set; }
        public string? CDAC { get; set; }
    }
}
