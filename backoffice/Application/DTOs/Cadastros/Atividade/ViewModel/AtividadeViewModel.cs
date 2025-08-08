namespace Application.DTOs.Cadastros.Atividade.ViewModel
{
    public class AtividadeViewModel
    {
        public decimal? ValorAplicacao { get; set; }
        public decimal? ValorIncendio { get; set; }
        public double? HorasAplicacao { get; set; }
        public double? HorasIncendio { get; set; }
        public double? HorasTranslado { get; set; }
        public double? Extensao { get; set; }
        public AtividadeRelatorioViewModel? AtividadeAplicacao { get; set; }
        public AtividadeRelatorioViewModel? AtividadeIncendio { get; set; }
    }
}
