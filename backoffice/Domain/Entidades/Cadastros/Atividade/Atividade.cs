namespace Domain.Entidades.Cadastros.Atividade
{
    public class Atividade
    {
        public double? TotalHorasAplicacao { get; set; }
        public double? TotalHorasIncendio { get; set; }
        public string? Extensao { get; set; }
        public string? ValorTotalIncendio { get; set; }
        public string? ValorTotalAplicacao { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
    }
}
