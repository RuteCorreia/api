using Application.DTOs.Users.ViewModel;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Interface;

public interface IUserAuthService
{
    Task<(bool, string)> LoginAsync(UserLoginViewModel user);
    Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel user);
    Task<Usuario> UpdateUserAsync(string id, UserLoginViewModel user);
    Task<IEnumerable<Usuario>> GetUsers();
    Task<Usuario> GetUserById(string id);
    Task RemoveUser(string id);
}