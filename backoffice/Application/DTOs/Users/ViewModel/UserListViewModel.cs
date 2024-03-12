using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTOs.Users.ViewModel;

public class UserListViewModel
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Credencial { get; set; }
    public string? Telefone { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ERole Funcao { get; set; }
}
