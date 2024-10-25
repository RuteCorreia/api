namespace Application.DTOs.Cadastros.Dashboard.ViewModel
{
    public class ExportDashboardViewModel
    {
        public string? Relatorio { get; set; }
        public int? Ano { get; set; }
        public string? Mes { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? Aeronave { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Cliente { get; set; }
        public string? Extensao { get; set; }
        public decimal? HectaresVoados { get; set; }
        public decimal? Faturamento { get; set; }
        public decimal? HorasVoadas { get; set; }
        public decimal? Rendimento { get; set; }
    }
}
