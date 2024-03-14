using Domain.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    public IEnumerable<RoleObject> Funcoes { get; set; } 
}

public class RoleObject
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole Funcao { get; set; }
    public string? Credencial { get; set; }
}
