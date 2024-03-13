using Application.DTOs.Users.ViewModel;
using Domain.Entidades.User;

namespace Application.DTOs.Users.Interface;

public interface IUserAuthService
{
    Task<(bool, string)> LoginAsync(UserLoginViewModel user);
    Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel user, string loggedUserId);
    Task<(bool, string)> ChangeUserPasswordAsync(UserChangePasswordViewModel user);
    Task<(bool, string)> UpdateUserAsync(string id, UserUpdateViewModel user);
    Task<IEnumerable<UserListViewModel>> GetAllUsersAsync(string loggedUserId);
    Task<UserDetailViewModel> GetUserByIdAsync(string id);
    Task RemoveUserAsync(string id);
}