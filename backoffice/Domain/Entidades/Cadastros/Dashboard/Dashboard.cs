namespace Domain.Entidades.Cadastros.Dashboard
{
    public class Dashboard
    {
        public string? NumeroDocumento { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal? ExtensaoTotal { get; set; }
        public string? Piloto { get; set; }
        public string? Executor { get; set; }
        public string? Aeronave { get; set; }
        public string? Cliente { get; set; }
        public DateTime? DataCriacao { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? TotalHoras { get; set; }
    }
}
