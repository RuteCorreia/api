using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Pistas.ViewModel;

public class PistaRelatorioIncendioViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string NomePista { get; set; }
    public string LatPista { get; set; }
    public string LongPista { get; set; }
}
