namespace Application.DTOs.Cadastros.FrotaGerador.ViewModel
{
    public class FrotaGeradorViewModel
    {
        public int? Id { get; set; }
        public int? IdGerador { get; set; }
        public int? IdFrota { get; set; }
        public DateTime? CreatedAt { get; set; }
        public double? HoraInicio { get; set; }
        public double? HoraFim { get; set; }
        public double? HorasUso { get; set; }
        public DateTime? DataTrocaOleo { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
