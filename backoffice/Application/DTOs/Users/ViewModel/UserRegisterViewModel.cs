using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.ViewModel;

public class UserRegisterViewModel
{
    [Required]
    [MinLength(3, ErrorMessage = "O nome deve conter pelo menos 3 caracteres no nome")]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres")]
    public string Password { get; set; }
    public string? Credencial { get; set; }
    public string? Telefone { get; set; }

    [Required]
    [MinLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
    [MaxLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
    public string CPF { get; set; }
    public decimal? Comissao { get; set; }
    public bool GerarRelatorioManutencao { get; set; }
    public IEnumerable<RoleObject> Funcoes { get; set; }

    public int IdCliente { get; set; }
}

public class RoleObject
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole Funcao { get; set; }
    public string? Credencial { get; set; }
    public string? NomeCompleto { get; set; }
}
