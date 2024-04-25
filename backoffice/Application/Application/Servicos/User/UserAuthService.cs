using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.User;
using Domain.Enums;
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
    private readonly IUsuarioCredencialRepository _usuarioCredencialRepository;
    private readonly IMapper _mapper;
    public UserAuthService(
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager,
        IConfiguration config, 
        IUsuarioRepository usuarioRepository,
        IUsuarioCredencialRepository usuarioCredencialRepository,
        IMapper mapper
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _usuarioRepository = usuarioRepository;
        _usuarioCredencialRepository = usuarioCredencialRepository;
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
                    //implementar isso aqui posteriormente
                    //if (usuario.PrimeiroAcesso)
                    //    token.Append("PrimeiroAcesso");
                    //else
                        token.Append(await GenerateToken(identityUser, usuario));

                    return (true, token.ToString());
                }
            }
        }
        return (false, "login inválido");
    }

    public async Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel request, string loggedUserId)
    {
        var resultMsg = new StringBuilder();

        if(VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(request.Funcoes))     
             return (false, resultMsg.Append("Não é permitido cadastrar usuário como piloto e executor.").ToString());

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

            var loggedUserTblUsuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);

            if(request.Funcoes.Any(x => x.Funcao == ERole.EngAgronomoCoord))
            {
                var jaExisteEngenheiro = await _usuarioCredencialRepository.VerificarSeEmpresaPossuiEngenheiroAtivo(loggedUserTblUsuario.IdEmpresa);
                if (jaExisteEngenheiro)
                {
                    await _userManager.DeleteAsync(identityUser);
                    return (false, resultMsg.Append("Engenheiro já existente na empresa").ToString());
                }
            }


            var roleListAsString = request.Funcoes.Select(r => r.Funcao.ToString());
            foreach (var role in roleListAsString)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                    await CreateRoleAsync(role);
            }

            var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, roleListAsString);
            if (!identityRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(identityUser);
                return (false, resultMsg.Append(GetIdentityResultErrors(identityRoleResult)).ToString());
            }

            
            await CreateUser(request, identityUser, loggedUserTblUsuario.IdEmpresa);
            await CreateUserCredencial(identityUser, request.Funcoes);

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
            request.Telefone,
            idEmpresa
            );

        await _usuarioRepository.AddAsync(usuario);
    }

    private async Task CreateUserCredencial(IdentityUser user, IEnumerable<RoleObject> roles)
    {
        var usuario = await _usuarioRepository.GetByUserIdAsync(user.Id);
        var list = Enumerable.Empty<UsuarioCredencial>();
        foreach(var role in roles)
        {
            var obj = new UsuarioCredencial
            {
                IdUsuario = usuario.Id,
                Credencial = role.Credencial,
                Funcao = role.Funcao
            };

            list = list.Concat(new[] { obj });
        }

        await _usuarioCredencialRepository.AddListAsync(list);
    }

    private bool VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(IEnumerable<RoleObject> funcoes)
    {
        return funcoes.Any(x => x.Funcao == ERole.Piloto) && funcoes.Any(x => x.Funcao == ERole.TecnicoExecutor);
    }

    private async Task<string> GenerateToken(IdentityUser identityUser, Usuario usuario)
    {
        var claims = await GetUserClaims(identityUser, usuario);
        var identityClaims = new ClaimsIdentity(claims);
        return WriteToken(identityClaims);
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(IdentityUser identityUser, Usuario usuario)
    {
        var roles = await _userManager.GetRolesAsync(identityUser);
        var claims = new List<Claim>
        {
            new("NrUsuario", usuario.NrUsuario.ToString()),
            new("IdUsuario", usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, identityUser.Id),
            new(JwtRegisteredClaimNames.Name, usuario.Nome),
            new(JwtRegisteredClaimNames.Email, identityUser.Email ?? "n/a"),
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

    public async Task<IEnumerable<UserListViewModel>> GetAllUsersAsync(string loggedUserId)
    {
        var loggedUserTblUsuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);
        var users = await _usuarioRepository.GetAllAsync(loggedUserTblUsuario.IdEmpresa);
        return _mapper.Map<IEnumerable<UserListViewModel>>(users);
    }

    public async Task RemoveUserAsync(string id)
    {
        var userToRemove = await GetUserEntityByIdAsync(id);
        if(userToRemove is not null)
        {
            userToRemove.Removido = true;
            await _usuarioRepository.UpdateAsync(userToRemove);
        }
    }

    private async Task<Usuario> GetUserEntityByIdAsync(string id) => await _usuarioRepository.GetUserByIdAsync(id);

    public async Task<UserDetailViewModel> GetUserByIdAsync(string id)
    {
        var obj = await _usuarioRepository.GetUserByIdAsync(id);
        var mapObjUsuario = _mapper.Map<UserDetailViewModel>(obj);
        var usuarioCredencialList = await _usuarioCredencialRepository.GetUsuarioCredencialsAsync(obj.Id);
        mapObjUsuario.Funcoes = _mapper.Map<IEnumerable<RoleObject>>(usuarioCredencialList);
        return mapObjUsuario;
    } 
   
    public async Task<(bool, string)> UpdateUserAsync(string id, UserUpdateViewModel request)
    {
        var resultMsg = new StringBuilder().Append("Atualização de usuário não foi possível");

        if (VerificarSeUsuarioEstaSendoCadastradoOuAtualizadoComoPiloto_E_Executor(request.Funcoes))
            return (false, resultMsg.Clear().Append("Não é permitido colocar o usuário como piloto e executor.").ToString());

        var userToUpdate = await GetUserEntityByIdAsync(id);
        if (userToUpdate is not null)
        {
            var identityUser = await _userManager.FindByEmailAsync(userToUpdate.Email);
            if(identityUser is not null)
            {
                resultMsg.Clear();
                bool allOk = true;

                if (request.Funcoes.Any(x => x.Funcao == ERole.EngAgronomoCoord))
                {
                    var jaExisteEngenheiro = await _usuarioCredencialRepository.VerificarSeEmpresaPossuiEngenheiroAtivo(userToUpdate.IdEmpresa, userToUpdate.Id.ToString());
                    if (jaExisteEngenheiro)
                    {
                        return (false, resultMsg.Append("Engenheiro já existente na empresa").ToString());
                    }
                }

                var identityUserRoles = await _userManager.GetRolesAsync(identityUser);
                if (identityUser.Email != request.Email)
                {                    
                    var changeEmailToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, request.Email, true);
                    var changeEmailResult = await _userManager.ChangeEmailAsync(identityUser, request.Email, changeEmailToken);
                    if (!changeEmailResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changeEmailResult));
                        allOk = false;
                    }
                }

                if(identityUser.PhoneNumber != request.Telefone)
                {
                    var changePhoneToken = await GenerateChangeEmailOrPhoneTokenAsync(identityUser, request.Telefone ?? "");
                    var changePhoneResult = await _userManager.ChangePhoneNumberAsync(identityUser, request.Telefone ?? "", changePhoneToken);
                    if (!changePhoneResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(changePhoneResult));                        
                        allOk = false;
                    }
                }

                if(identityUser.UserName != request.Email)
                {
                    identityUser.UserName = request.Email;
                    var updateIdentityUserResult = await _userManager.UpdateAsync(identityUser);
                    if (!updateIdentityUserResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(updateIdentityUserResult));
                        allOk = false;
                    }
                }                               

                var requestRole = request.Funcoes.Select(r => r.Funcao.ToString());
                var rolesNotInIdentity = requestRole.Except(identityUserRoles);
                if (rolesNotInIdentity.Any())
                {
                    foreach (var role in rolesNotInIdentity)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync(role);
                        if (!roleExists)
                            await CreateRoleAsync(role);
                    }

                    await _userManager.RemoveFromRolesAsync(identityUser, identityUserRoles);
                    var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, requestRole);
                    if (!identityRoleResult.Succeeded)
                    {
                        resultMsg.Append(GetIdentityResultErrors(identityRoleResult));
                        allOk = false;
                    }
                }

                if (allOk)
                {
                    userToUpdate.Nome = request.Nome;
                    userToUpdate.Email = request.Email;
                    userToUpdate.Telefone = request.Telefone;
                    await _usuarioRepository.UpdateAsync(userToUpdate);
                    await _usuarioCredencialRepository.RemoveAllByUserIdAsync(userToUpdate.Id);
                    await CreateUserCredencial(identityUser, request.Funcoes);
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

    public async Task<IEnumerable<RoleObject>> GetUserRolesAsync(string id)
    {
        var userRoles = await _usuarioCredencialRepository.GetUsuarioCredencialsAsync(Guid.Parse(id));
        var returnList = Enumerable.Empty<RoleObject>();
        foreach(var item in userRoles)
        {
            var obj = new RoleObject
            {
                Funcao = item.Funcao,
                Credencial = item.Credencial
            };

            returnList = returnList.Concat(new[] { obj });
        }

        return returnList;
    }

    public async Task<(bool, string)> SaveUserSignatureAsync(UserSaveSignatureViewModel obj, string loggedUserId)
    {
        var resultMsg = new StringBuilder().Append("Não foi possível salvar assinatura");
        try
        {
            var loggedIdentityUser = await _userManager.FindByIdAsync(loggedUserId);
            if (loggedIdentityUser is not null)
            {
                var usuario = await _usuarioRepository.GetByUserIdAsync(loggedUserId);
                if (usuario is not null)
                {
                    byte[] bytes = Convert.FromBase64String(obj.Assinatura);
                    usuario.Assinatura = bytes;
                    await _usuarioRepository.UpdateAsync(usuario);
                    return (true, resultMsg.Clear().ToString());
                }
            }

            return (false, resultMsg.ToString());
        }
        catch(Exception ex)
        {
            return (false, resultMsg.Clear().Append(ex.Message).ToString());
        }
    }

    public async Task<string> GetUserSignatureAsync(string userId)
    {
        var result = new StringBuilder();
        var usuario = await _usuarioRepository.GetUserByIdAsync(userId);
        if(usuario is not null && usuario.Assinatura is not null)
        {
            string convertedStr = Convert.ToBase64String(usuario.Assinatura);
            result.Append(convertedStr);
        }

        return result.ToString(); 
    }

    public async Task<(bool, string)> RegisterUserFromEmpresaAsync(UserRegisterViewModel request, int? idEmpresa)
    {
        var resultMsg = new StringBuilder();

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

        var roleListAsString = request.Funcoes.Select(r => r.Funcao.ToString());
        foreach (var role in roleListAsString)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
                await CreateRoleAsync(role);
        }

        var identityRoleResult = await _userManager.AddToRolesAsync(identityUser, roleListAsString);
        if (!identityRoleResult.Succeeded)
        {
            await _userManager.DeleteAsync(identityUser);
            return (false, resultMsg.Append(GetIdentityResultErrors(identityRoleResult)).ToString());
        }


        await CreateUser(request, identityUser, idEmpresa);
        await CreateUserCredencial(identityUser, request.Funcoes);

        return (true, resultMsg.Append("Usuário criado com sucesso").ToString());      
    }
}
