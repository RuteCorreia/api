using Domain.Enums;

namespace Application.DTOs.Users.ViewModel;

public class UserDetailViewModel
{
    public string Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }
    public string? Credencial { get; set; }
    public string? Telefone { get; set; }

    public ERole Funcao { get; set; }
}
