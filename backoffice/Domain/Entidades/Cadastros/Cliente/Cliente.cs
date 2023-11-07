using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Cliente;

public class Cliente
{
    [Key]
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public int? IdTipoCliente { get; set; }
    public int? CPF { get; set; }
    public int? RG { get; set; }
    public int? CNPJ { get; set; }
    public int? InscricaoEstadual { get; set; }
    public string Endereco { get; set; }

    [Required]
    public string Telefone1 { get; set; }
    public string Telefone2 { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    public string Precificacao { get; set; }
}
