using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Piloto.ViewModel;

public class PilotoViewModel
{
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Senha { get; set; }

    [Required]
    public string CANAC { get; set; }

    public string? Telefone { get; set; }

    [Required]
    public string Assinatura { get; set; }

    [Required]
    public int PorcentagemComissao { get; set; }
}
