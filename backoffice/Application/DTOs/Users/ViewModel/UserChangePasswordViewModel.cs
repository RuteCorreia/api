using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users.ViewModel;

public class UserChangePasswordViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    //[Required]
    //[DataType(DataType.Password)]
    //public string OldPassword { get; set; }


    [Required]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres")]
    public string NewPassword { get; set; }

    [Required]
    public string Token { get; set; }
}
