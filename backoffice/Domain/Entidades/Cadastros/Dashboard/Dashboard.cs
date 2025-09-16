namespace Domain.Entidades.Cadastros.Dashboard
{
    public class Dashboard
    {
        public string? NumeroDocumento { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal? ExtensaoTotal { get; set; }
        public decimal? ExtensaoDrone { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Aeronave { get; set; }
        public string? Cliente { get; set; }
        public DateTime? DataCriacao { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? TotalHoras { get; set; }
        public double? TotalHorasAplicacao { get; set; }
        public double? TotalHorasIncendio { get; set; }
        public double? TotalHorasTranslado { get; set; }
        public double? TotalHorasDrone { get; set; }
        public bool? IsDrone { get; set; }
    }
}
