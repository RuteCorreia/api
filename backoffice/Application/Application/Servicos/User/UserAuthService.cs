using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;
using Domain.Interfaces.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Application.Servicos.User;

public class UserAuthService : IUserAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    public UserAuthService(
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        IConfiguration config, 
        IUsuarioRepository usuarioRepository,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<(bool, string)> LoginAsync(UserLoginViewModel user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if(identityUser is not null)
        {            
            var usuario = await _usuarioRepository.GetByUserIdAsync(identityUser.Id);
            if(usuario is not null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                if (passwordCheck)
                {
                    var token = new StringBuilder();
                    if (usuario.PrimeiroAcesso)
                        token.Append("PrimeiroAcesso");
                    else
                        //token.Append(await GenerateToken(identityUser, usuario.Nome, usuario.NrUsuario));
                        token.Append(await GenerateToken(identityUser, "teste", 1));

                    return (true, token.ToString());
                }
            }
        }
        return (false, "login inválido");
    }

    public async Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel request, string loggedUserId)
    {
        var resultMsg = new StringBuilder();
        var loggedIdentityUser = await _userManager.FindByIdAsync(loggedUserId);
        if(loggedIdentityUser is not null)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Telefone,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

            if (!identityResult.Succeeded)
                return (false, resultMsg.Append(GetIdentityResultErrors(identityResult)).ToString());

            var roleExists = await _roleManager.RoleExistsAsync(request.Role.ToString());
            if (!roleExists)
            {
                await CreateRoleAsync(request.Role.ToString());
            }

            var identityRoleResult = await _userManager.AddToRoleAsync(identityUser, request.Role.ToString());
            if (!identityRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(identityUser);
                return (false, resultMsg.Append(GetIdentityResultErrors(identityRoleResult)).ToString());
            }

            var loggedUserTblUsuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);
            await CreateUser(request, identityUser, loggedUserTblUsuario.IdEmpresa);

            return (true, resultMsg.Append("Usuário criado com sucesso").ToString());
        }

        return (false, resultMsg.Append("Não foi possível criar um usuário").ToString());
    }

    private async Task CreateUser(UserRegisterViewModel request, IdentityUser user, int? idEmpresa)
    {
        var nrUsuarioOrdem = await _usuarioRepository.GetLastAsync();
        var usuario = new Usuario(
            request.Email,
            request.Name, 
            user.Id, 
            nrUsuarioOrdem is null ? 1 : nrUsuarioOrdem.NrUsuario + 1,
            request.Credencial,
            request.Telefone,
            request.Role.ToString(),
            idEmpresa
            );

        await _usuarioRepository.AddAsync(usuario);
    }

    private async Task<string> GenerateToken(IdentityUser user, string userName, int nrUsuario)
    {
        var claims = await GetUserClaims(user, userName, nrUsuario);
        var identityClaims = new ClaimsIdentity(claims);
        return WriteToken(identityClaims);
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(IdentityUser user, string userName, int nrUsuario)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new("NrUsuario", nrUsuario.ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, userName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
            new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private string WriteToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var expiresInHours = Convert.ToDouble(_config.GetSection("Jwt:ExpirationInHours").Value);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _config.GetSection("Jwt:Issuer").Value,
            Audience = _config.GetSection("Jwt:Audience").Value,
            Expires = DateTime.UtcNow.AddHours(expiresInHours),
            SigningCredentials = signingCredentials
        });

        return tokenHandler.WriteToken(token);
    }

    private long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);

    public async Task<IEnumerable<UserListViewModel>> GetAllUsersAsync()
    {
        var users = await _usuarioRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserListViewModel>>(users);
    }

    public async Task RemoveUserAsync(string id)
    {
        var userToRemove = await GetUserByIdAsync(id);
        if(userToRemove is not null)
        {
            userToRemove.Removido = true;
            await _usuarioRepository.UpdateAsync(userToRemove);
        }
    }

    public async Task<Usuario> GetUserByIdAsync(string id) => await _usuarioRepository.GetUserByIdAsync(id);
   
    public async Task<(bool, string)> UpdateUserAsync(string id, UserUpdateViewModel request)
    {
        var resultMsg = new StringBuilder().Append("Atualização de usuário não foi possível");
        var userToUpdate = await GetUserByIdAsync(id);
        if (userToUpdate is not null)
        {
            var identityUser = await _userManager.FindByEmailAsync(userToUpdate.Email);
            if(identityUser is not null)
            {
                resultMsg.Clear();
                bool allOk = true;
                var identityUserRoles = await _userManager.GetRolesAsync(identityUser);
                if (identityUser.Email != request.Email)
                {                    
                    var changeEmailToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, userToUpdate.Email, true);
                    var changeEmailResult = await _userManager.ChangeEmailAsync(identityUser, request.Email, changeEmailToken);
                    if (!changeEmailResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changeEmailResult));
                        allOk = false;
                    }
                }

                if(identityUser.PhoneNumber != request.Telefone)
                {
                    var changePhoneToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, userToUpdate.Telefone ?? "");
                    var changePhoneResult = await _userManager.ChangePhoneNumberAsync(identityUser, request.Telefone ?? "", changePhoneToken);
                    if (!changePhoneResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changePhoneResult));                        
                        allOk = false;
                    }
                }

                if(identityUser.UserName != request.Name)
                {
                    identityUser.UserName = request.Name;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(updateIdentityUserResult));
                        allOk = false;
                    }
                }

                var requestRole = request.Role.ToString();
                if (!identityUserRoles.Contains(requestRole))
                {
;                   var roleExists = await _roleManager.RoleExistsAsync(requestRole);
                    if(!roleExists)
                    {
                        await CreateRoleAsync(requestRole);
                    }
                    await _userManager.RemoveFromRolesAsync(identityUser, identityUserRoles);
                    var identityRoleResult = await _userManager.AddToRoleAsync(identityUser, requestRole);
                    if (!identityRoleResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(identityRoleResult));
                        allOk = false;
                    }
                }

                if (allOk)
                {
                    userToUpdate.Nome = request.Name;
                    userToUpdate.Email = request.Email;
                    userToUpdate.Telefone = request.Telefone;
                    userToUpdate.Credencial = request.Credencial;
                    await _usuarioRepository.UpdateAsync(userToUpdate);
                    resultMsg.Append("Sucesso na atualização do usuário");
                    return (true, resultMsg.ToString());
                }
            }
        }

        return (false, resultMsg.ToString());
    }

    private async Task<string> GenerateChangeEmailOrPhoneTokenAsync(IdentityUser user, string item, bool email = false)
    {
        if (email) 
            return await _userManager.GenerateChangeEmailTokenAsync(user, item);
        else 
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, item);
    }

    private string GetIdentityResultErrors(IdentityResult result)
    {
        var errors = result.Errors
            .Select(x => x.Description)
            .FirstOrDefault();

        return !string.IsNullOrEmpty(errors) ? errors : "Erro no processamento";
    }


    private async Task CreateRoleAsync(string roleToCreate)
    {
        var role = new IdentityRole(roleToCreate);
        await _roleManager.CreateAsync(role);
    }

    public async Task<(bool, string)> ChangeUserPasswordAsync(UserChangePasswordViewModel user)
    {
        var resultMsg = new StringBuilder().Append("Troca de senha não foi possível");
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser is not null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.OldPassword);
            if (passwordCheck)
            {
                resultMsg.Clear();
                bool allOk = true;
                var identityResult = await _userManager.ChangePasswordAsync(identityUser, user.OldPassword, user.NewPassword);
                if (!identityResult.Succeeded)
                {
                    resultMsg.Append(GetIdentityResultErrors(identityResult));
                    allOk = false;
                }

                if (allOk)
                    resultMsg.Append("Sucesso na troca de senha");

                return (allOk, resultMsg.ToString());
            }
        }

        return (false, resultMsg.ToString());
    }
}
