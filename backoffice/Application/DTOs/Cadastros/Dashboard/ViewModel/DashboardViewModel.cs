namespace Application.DTOs.Cadastros.Dashboard.ViewModel
{
    public class DashboardViewModel
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public decimal? ExtensaoTotal { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? TotalHoras { get; set; }
        public decimal? Rendimento { get; set; }
    }
}
