using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.ViewModel;

public class UserUpdateViewModel
{
    public string Id { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "O nome deve conter pelo menos 3 caracteres no nome")]
    public string Nome { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public string? Credencial { get; set; }
    public string? Telefone { get; set; }

    [Required]
    [MinLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
    [MaxLength(11, ErrorMessage = "CPF deve ter 11 dígitos")]
    public string CPF { get; set; }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public IEnumerable<RoleObject> Funcoes { get; set; }
}
