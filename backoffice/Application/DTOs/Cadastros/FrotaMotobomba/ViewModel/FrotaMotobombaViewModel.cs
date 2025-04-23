namespace Application.DTOs.Cadastros.FrotaMotobomba.ViewModel
{
    public class FrotaMotobombaViewModel
    {
        public int? Id { get; set; }
        public int? IdFrota { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Identificacao { get; set; }
        public double? LitrosOleo { get; set; }
        public double? LitrosGasolina { get; set; }
        public string? CheckList { get; set; }
        public int? IdEmpresa { get; set; }
        public string? NomeMotobomba { get; set; }
    }
}
