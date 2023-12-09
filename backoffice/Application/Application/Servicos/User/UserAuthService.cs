using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
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
    public UserAuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IUsuarioRepository usuarioRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> LoginAsync(UserLoginViewModel user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if(identityUser is not null)
        {
            var usuario = await _usuarioRepository.GetByUserIdAsync(identityUser.Id);
            if(usuario is not null)
            {
                return !usuario.Removido ? await _userManager.CheckPasswordAsync(identityUser, user.Password) : false;
            }
            
        }
        return false;
    }

    public async Task<(bool, string)> RegisterUserAsync(UserRegisterViewModel request)
    {
        var identityUser = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

        if(!identityResult.Succeeded)
        {
            var errors = identityResult.Errors
                .Select(x => x.Description)
                .FirstOrDefault();
            
            return !string.IsNullOrEmpty(errors) ?  (false,  errors) : (false, "Erro na criação de novo usuário");
        }

        var roleExists = await _roleManager.RoleExistsAsync(ERole.Client.ToString());
        if (!roleExists)
        {
            var role = new IdentityRole(ERole.Client.ToString());
            await _roleManager.CreateAsync(role);
        }

        var identityRoleResult = await _userManager.AddToRoleAsync(identityUser, ERole.Client.ToString());
        if (!identityRoleResult.Succeeded)
        {
            var errors = identityRoleResult.Errors
                .Select(x => x.Description)
                .FirstOrDefault();

            await _userManager.DeleteAsync(identityUser);

            return !string.IsNullOrEmpty(errors) ? (false, errors) : (false, "Erro na criação de novo usuário");
        }

        var cliente = CreateUser(request, identityUser);

        return (true, "Usuário criado com sucesso");
    }

    private async Task CreateUser(UserRegisterViewModel request, IdentityUser user)
    {
        var usuario = new Usuario(request.Email, request.Name, user.Id);
        await _usuarioRepository.AddAsync(usuario);
    }

    public string GenerateTokenString(UserLoginViewModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value);
        var expiresInHours = Convert.ToDouble(_config.GetSection("Jwt:ExpirationInHours").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Client")
            }),
            Issuer = _config.GetSection("Jwt:Issuer").Value,
            Audience = _config.GetSection("Jwt:Audience").Value,
            Expires = DateTime.UtcNow.AddHours(expiresInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
