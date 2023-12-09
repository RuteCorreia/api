using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _authService;

    public AuthController(IUserAuthService authService)
    {
        _authService = authService;
    }

    #region TESTES
    //DESCOMENTAR PRA TESTE
    //[HttpGet("anonimo")]
    //[AllowAnonymous]
    //public async Task<string> Anonimo() => "anonimo lixo";

    //[HttpGet("authorized")]
    //[Authorize]
    //public async Task<string> Authorize() => "autorizado";

    //[HttpGet("authorizedWithClientRole")]
    //[Authorize(Roles = "Client")]
    //public async Task<string> AuthorizeClient() => "cliente autorizado";

    //[HttpGet("authorizedWithAdminRole")]
    //[Authorize(Roles = "Admin")]
    //public async Task<string> AuthorizeAdm() => "admin autorizado";

    #endregion

    [HttpPost("registerUser")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.RegisterUserAsync(user);
            if (result.Item1)
                return Ok(result.Item2);

            return BadRequest(result.Item2);
        }
        return BadRequest("campos de registro não válidos");        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(user);
            if (result)
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
        }

        return BadRequest();
    }
}