using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Cliente.ViewModel;

public class ClienteViewModel
{
    public int IdCliente { get; set; }
    public string NomeCliente { get; set; }
    public int? IdTipoCliente { get; set; }
    public int? CPF { get; set; }
    public int? RG { get; set; }
    public int? CNPJ { get; set; }
    public int? InscricaoEstadual { get; set; }
    public string Endereco { get; set; }
    public string Telefone1 { get; set; }
    public string Telefone2 { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Precificacao { get; set; }

    public bool Admin { get; set; }
}
