using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.User;

public class RegistrationModel
{
    [Required(ErrorMessage = "")]
    public string Username { get; set; }

    [Required(ErrorMessage = "")]
    public string Nome { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "")]
    public string Email { get; set; }

    [Required(ErrorMessage = "")]
    public string Senha { get; set; }
}
