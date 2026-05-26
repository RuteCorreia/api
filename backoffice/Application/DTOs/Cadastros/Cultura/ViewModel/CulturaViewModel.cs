using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Cultura.ViewModel;

public class CulturaViewModel
{
    public int IdCultura { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string? Nome { get; set; }
    public string? AlvoBiologico { get; set; }
    public int? IdEmpresa { get; set; }
    public string? Status { get; set; }
}
