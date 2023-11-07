using Domain.Entidades.User;

namespace Domain.Interfaces.User;

public interface IAuthService
{
    Task<(int, string)> RegisterAsync(RegistrationModel model, string role);
    Task<(int, string)> LoginAsync(LoginModel model);
}
