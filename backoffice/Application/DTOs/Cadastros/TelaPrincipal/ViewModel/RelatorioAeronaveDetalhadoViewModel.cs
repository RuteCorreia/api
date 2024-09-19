namespace Application.DTOs.Cadastros.TelaPrincipal.ViewModel
{
    public class RelatorioAeronaveDetalhadoViewModel
    {
        public string Aeronave { get; set; }
        public decimal ExtensaoTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalHoras { get; set; }
        public decimal Rendimento { get; set; }
        public decimal ValorHorasVoadas { get; set; }

        public List<ComissaoViewModel> Comissoes { get; set; }
        public decimal HorasDisponiveisRevisao { get; set; }

        // Formatações dos dados
        public string ExtensaoTotalFormatado => $"{ExtensaoTotal:N0} ha";
        public string ValorTotalFormatado => $"R$ {ValorTotal:N2}";
        public string TotalHorasFormatado => $"{TotalHoras:N2} horas";
        public string RendimentoFormatado => $"{Rendimento:N2} ha/hora";
        public string ValorHorasVoadasFormatado => $"R$ {ValorHorasVoadas:N2}";
    }
}
