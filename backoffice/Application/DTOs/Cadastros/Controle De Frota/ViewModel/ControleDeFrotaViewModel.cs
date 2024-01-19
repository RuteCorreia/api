using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;

public class ControleDeFrotaViewModel
{
    public int Id { get; set; }
    public string? Observacao { get; set; }
    public DateTime? Data { get; set; }
    public int? IdFrota { get; set; }
    public int? IdAeronave { get; set; }
    public int? KmInicial { get; set; }
    public int? LocalInicial { get; set; }
    public string? LocalizacaoPistaLat { get; set; }
    public string? LocalizacaoPistaLon { get; set; }
    public int? KmFinal { get; set; }
    public int? HorimetroInicial { get; set; }
    public int? HorimetroFinal { get; set; }
    public string? Combustivel { get; set; }
    public int? QtdeCombustivel { get; set; }
    public int? QtdeHectare { get; set; }
    public int? IdPiloto { get; set; }
}
