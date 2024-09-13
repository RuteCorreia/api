namespace Application.DTOs.Cadastros.TelaPrincipal.ViewModel
{
    public class RelatorioAeronaveViewModel
    {
        public string NomeAeronave { get; set; }
        public decimal ExtensaoTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalHoras { get; set; }
        public decimal? Rendimento { get; set; }
        public decimal ValorHorasVoadas { get; set; }

        // Propriedades formatadas
        public string ExtensaoTotalFormatado { get; set; }
        public string ValorTotalFormatado { get; set; }
        public string TotalHorasFormatado { get; set; }
        public string RendimentoFormatado { get; set; }
        public string ValorHorasVoadasFormatado { get; set; }
    }
}
