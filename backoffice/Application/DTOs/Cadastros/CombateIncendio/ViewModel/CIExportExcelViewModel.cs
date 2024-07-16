namespace Application.DTOs.Cadastros.CombateIncendio.ViewModel
{
    public class CIExportExcelViewModel
    {
        public string? UF { get; set; }
        public string? Cidade { get; set; }
        public string? Prefixo { get; set; }
        public string? Modelo { get; set; }
        public decimal? TotalAguaUtilizadaOperacao { get; set; }
        public string? HorasAplicacao { get; set; }
    }
}
