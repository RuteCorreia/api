namespace Domain.Entidades.Cadastros.Piloto
{
    public class Piloto
    {
        public Guid IdPiloto { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public byte[]? Assinatura { get; set; }
        public string? CDAC { get; set; }
        public DateTime DataSituacao { get; private set; } =
            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
