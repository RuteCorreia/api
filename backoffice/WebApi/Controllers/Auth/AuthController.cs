using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using Domain.Entidades.User;
using Domain.Interfaces.Genericos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _authService;

    public AuthController(IUserAuthService authService)
    {
        _authService = authService;
    }
   
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
            if (result.Item1)
            {
                return Ok(new { success = true,  token = result.Item2 });
            }

            return BadRequest(result.Item2);
        }

        return BadRequest("Campos de login inválidos");
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
            var result = await _authService.GetUsers();
            if (result.Any())
            {
                return Ok(result);
            }

            return BadRequest(result);
    }

    [HttpDelete("RemoveUser")]
    public async Task<IActionResult> RemoveUser(string userId)
    {
        var result = _authService.RemoveUser(userId);
        if (result != null)
        {
            return Ok();
        }

        return BadRequest(result);
    }
}