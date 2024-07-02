using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;

public class CombateIncendioDecolagemPousoViewModel
{
    public int Id { get; set; }
    public int? IdCombateIncendio { get; set; }
    public DateTime? DecolagemHorario { get; set; }
    public string? DecolagemHorimetro { get; set; }
    public DateTime? PousoHorario { get; set; }
    public string? PousoHorimetro { get; set; }
    public string? IdEmpresa { get; set; }
}
