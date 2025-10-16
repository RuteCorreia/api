using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Piloto.ViewModel;

public class PilotoViewModel
{
    public string IdPiloto { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? Telefone { get; set; }
    public string? Assinatura { get; set; }
    public string? CDAC { get; set; }
    public string? Role { get; set; }
}
