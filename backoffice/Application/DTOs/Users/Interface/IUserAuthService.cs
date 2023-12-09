using Application.DTOs.Users.ViewModel;

namespace Application.DTOs.Users.Interface;

public interface IUserAuthService
{
    Task<bool> LoginAsync(UserLoginViewModel user);
    Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel user); 
    string GenerateTokenString(UserLoginViewModel user);
}
