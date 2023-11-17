using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.User;

public class LoginModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Senha { get; set; }
}
