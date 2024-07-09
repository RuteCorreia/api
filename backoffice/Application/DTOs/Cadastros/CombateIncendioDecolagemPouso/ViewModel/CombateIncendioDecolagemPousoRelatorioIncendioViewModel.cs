using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;

public class CombateIncendioDecolagemPousoRelatorioIncendioViewModel
{
    public int Id { get; set; }
    public DateTime? HorarioDecolagem { get; set; }
    public string? HorimetroDecolagem { get; set; }
    public DateTime? HorarioPouso { get; set; }
    public string? HorimetroPouso { get; set; }
}
