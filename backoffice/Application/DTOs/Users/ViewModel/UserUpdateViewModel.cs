using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole Funcao { get; set; }
}
