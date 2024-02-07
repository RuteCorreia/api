using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Cliente;

public class Cliente
{
    [Key]
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public int? IdTipoCliente { get; set; }
    public string? CPF { get; set; }
    public string? RG { get; set; }
    public string? CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string Endereco { get; set; }

    [Required]
    public string Telefone1 { get; set; }
    public string Telefone2 { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    public string? Senha { get; set; }
    public string? Cidade { get; set; }
    public string? UF { get; set; }
    public string Precificacao { get; set; }

    public bool Admin { get; set; }
}
