using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Veiculante.ViewModel;

public class VeiculanteViewModel
{
    public int IdVeiculante { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
}
