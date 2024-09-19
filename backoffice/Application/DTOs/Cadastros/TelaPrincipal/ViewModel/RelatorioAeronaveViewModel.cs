namespace Application.DTOs.Cadastros.TelaPrincipal.ViewModel
{
    public class RelatorioAeronaveViewModel
    {
        // Propriedades básicas
        public string Aeronave { get; set; } // Nome ou identificador da aeronave (ex: PR-IAA - AVIAO)
        public string Piloto { get; set; }   // Nome do piloto
        public string Executor { get; set; } // Nome do técnico executor

        // Dados de extensões e valores
        public decimal ExtensaoTotal { get; set; }  // Extensão total tratada pela aeronave (em hectares)
        public decimal ValorTotal { get; set; }     // Valor total do trabalho executado
        public decimal TotalHoras { get; set; }     // Total de horas voadas
        public decimal? Rendimento { get; set; }    // Rendimento calculado (ha/hora)
        public decimal ValorHorasVoadas { get; set; } // Valor por hora voada

        // Propriedades formatadas para exibição
        public string ExtensaoTotalFormatado { get; set; } // Ex: "13.128 ha"
        public string ValorTotalFormatado { get; set; }    // Ex: "R$ 113.362,00"
        public string TotalHorasFormatado { get; set; }    // Ex: "14,33 horas"
        public string RendimentoFormatado { get; set; }    // Ex: "915,91 ha/hora"
        public string ValorHorasVoadasFormatado { get; set; } // Ex: "R$ 7.908,98"
    }
}
