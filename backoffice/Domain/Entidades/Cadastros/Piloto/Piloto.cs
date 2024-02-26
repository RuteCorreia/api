using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Piloto;

public class Piloto
{
    [Key]
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
    public int PorcentagemComissao { get; set; }

    [Required]
    public string Assinatura { get; set; }
}