using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Engenheiro.ViewModel;

public class EngenheiroViewModel
{
    public int Id { get; set; }

    [Required]
    public int IdEmpresa { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    [PasswordPropertyText]
    public string Senha { get; set; }

    [Required]
    [MinLength(8)]
    public string CREA { get; set; }
    
    [Required]
    public int PorcentagemComissao { get; set; }

    [Required]
    public string Assinatura { get; set; }

    public string Telefone { get; set; }
}
