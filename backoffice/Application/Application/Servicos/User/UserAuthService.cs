using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
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
    public UserAuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IUsuarioRepository usuarioRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<(bool, string)> LoginAsync(UserLoginViewModel user)
    {
        var resultError = "login inválido";
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if(identityUser is not null)
        {            
            var usuario = await _usuarioRepository.GetByUserIdAsync(identityUser.Id);
            if(usuario is not null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(identityUser, user.Password);
                if (passwordCheck)
                {
                    var token = await GenerateToken(identityUser, usuario.Nome, usuario.NrUsuario);
                    return (true, token);
                }
            }
        }
        return (false, resultError);
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

        var roleExists = await _roleManager.RoleExistsAsync(request.Role.ToString());
        if (!roleExists)
        {
            var role = new IdentityRole(request.Role.ToString());
            await _roleManager.CreateAsync(role);
        }

        var identityRoleResult = await _userManager.AddToRoleAsync(identityUser, request.Role.ToString());
        if (!identityRoleResult.Succeeded)
        {
            var errors = identityRoleResult.Errors
                .Select(x => x.Description)
                .FirstOrDefault();

            await _userManager.DeleteAsync(identityUser);

            return !string.IsNullOrEmpty(errors) ? (false, errors) : (false, "Erro na criação de novo usuário");
        }

       await CreateUser(request, identityUser);

        return (true, "Usuário criado com sucesso");
    }

    private async Task CreateUser(UserRegisterViewModel request, IdentityUser user)
    {
        var nrUsuarioOrdem = await _usuarioRepository.GetLastAsync();
        var usuario = new Usuario(request.Email, request.Name, user.Id, nrUsuarioOrdem is null ? 1 : nrUsuarioOrdem.NrUsuario + 1);
        await _usuarioRepository.AddAsync(usuario);
    }

    private async Task<IEnumerable<Usuario>> GetUsers()
    {
        var users = await _usuarioRepository.GetAllAsync();
        return users;
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

    Task<IEnumerable<Usuario>> IUserAuthService.GetUsers()
    {
        var users = _usuarioRepository.GetAllAsync();
        return users;
    }
}
