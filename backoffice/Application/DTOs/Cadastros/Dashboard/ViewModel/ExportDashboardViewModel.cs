namespace Application.DTOs.Cadastros.Dashboard.ViewModel
{
    public class ExportDashboardViewModel
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Usuario { get; set; }
        public string? Aeronave { get; set; }
        public string? Cliente { get; set; }
        public string? Mes { get; set; }
        public int? Ano { get; set; }
        public decimal? Faturamento { get; set; }
        public decimal? HorasVoadas { get; set; }
        public decimal? HectaresVoados { get; set; }
        public decimal? Rendimento { get; set; }
    }
}
