namespace Application.DTOs.Cadastros.Atividade.ViewModel
{
    public class AtividadeRelatorioViewModel
    {
        public decimal? ValorTotal { get; set; }
        public string? TotalHoras { get; set; }
        public double? Extensao { get; set; }
        public List<ComissaoViewModel>? ComissaoPiloto { get; set; }
        public List<ComissaoViewModel>? ComissaoExecutor { get; set; }
    }
}
