using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.ViewModel;

public class UserLoginViewModel
{
    public string? Id { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres")]
    public string Password { get; set; }
}
