using Application.DTOs.Users.ViewModel;

namespace Application.DTOs.Users.Interface;

public interface IUserAuthService
{
    Task<(bool, string)> LoginAsync(UserLoginViewModel user);
    Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel user); 
    string GenerateTokenString(UserLoginViewModel user, string role);
}
