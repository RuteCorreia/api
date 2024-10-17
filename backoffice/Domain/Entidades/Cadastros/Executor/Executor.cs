namespace Domain.Entidades.Cadastros.Executor
{
    public class Executor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public byte[]? Assinatura { get; set; }
        public string? CFTA { get; set; }
    }
}
