using Application.DTOs.Users.ViewModel;

namespace Application.DTOs.Users.Interface;

public interface IUserAuthService
{
    Task<(bool, string)> LoginAsync(UserLoginViewModel user);
    Task<(bool, string)> LoginBackofficeAsync(UserLoginViewModel user);
    Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel user, string loggedUserId);
    Task<(bool, string)> RegisterUserFromEmpresaAsync(UserRegisterViewModel user, int? idEmpresa);
    Task<(bool, string)> ChangeUserPasswordAsync(UserChangePasswordViewModel user);
    Task<(bool, string)> UpdateUserAsync(string id, UserUpdateViewModel user);
    Task<IEnumerable<RoleObject>> GetUserRolesAsync(string id);
    Task<IEnumerable<UserListViewModel>> GetAllUsersAsync(string loggedUserId);
    Task RecoveryUserAsync(int id);
    Task<UserProfileViewModel> GetUserProfileAsync(string userId);
    Task<UserDetailViewModel> GetUserByIdAsync(string id);
    Task RemoveUserAsync(string id);
    Task<(bool, string)> SaveUserSignatureAsync(UserSaveSignatureViewModel obj, string loggedUserId);
    Task<string> GetUserSignatureAsync(string userId);
}