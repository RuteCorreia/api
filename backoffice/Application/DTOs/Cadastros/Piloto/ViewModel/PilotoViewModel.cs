using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Piloto.ViewModel;

public class PilotoViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    //[Required]
    //[MinLength(8)]
    //public string CREA { get; set; }

    public string? Telefone { get; set; }
}
