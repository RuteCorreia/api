using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.Auth;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _authService;
    private readonly LoggedUserInfoService _loggedUserInfoService;

    public AuthController(IUserAuthService authService, LoggedUserInfoService loggedUserInfoService)
    {
        _authService = authService;
        _loggedUserInfoService = loggedUserInfoService;
    }
   
    [HttpPost("registerUser")]
    [Authorize]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterViewModel user)
    {
        var resultError = new StringBuilder().Append("Campos de registro não válidos");
        var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole(); 
        if (ModelState.IsValid && !string.IsNullOrEmpty(loggedUser.Item1))
        {
            var result = await _authService.RegisterUserAsync(user, loggedUser.Item1);
            if (result.Item1)
                return Ok();

            resultError.Clear();
            resultError.Append(result.Item2);
        }

        return BadRequest(resultError.ToString());        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel user)
    {
        var resultError = new StringBuilder().Append("Campos de login inválidos");
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(user);
            if (result.Item1)
                return Ok(new { success = true,  token = result.Item2 });

            resultError.Clear();
            resultError.Append(result.Item2);
        }

        return BadRequest(resultError.ToString());
    }

    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
        return !string.IsNullOrEmpty(loggedUser.Item1) ? Ok(await _authService.GetAllUsersAsync(loggedUser.Item1)) : BadRequest();
    }

    [HttpPatch("changePassword")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordViewModel user)
    {
        var resultError = new StringBuilder().Append("Não foi possível alterar a senha");
        if (ModelState.IsValid)
        {
            var result = await _authService.ChangeUserPasswordAsync(user);
            if (result.Item1)
                return Ok();

            resultError.Clear();
            resultError.Append(result.Item2);
        }

        return BadRequest(resultError.ToString());
    }

    [HttpPut("updateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string id,[FromBody] UserUpdateViewModel user)
    {
        var resultError = new StringBuilder().Append("Falha ao atualizar usuário");
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id) && ModelState.IsValid)
        {
            var result = await _authService.UpdateUserAsync(id, user);
            if (result.Item1)
                return Ok();

            resultError.Clear();
            resultError.Append(result.Item2);
        }

        return BadRequest(resultError.ToString());
    }

    [HttpGet("GetUserById")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string userId)
    {
        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrWhiteSpace(userId))
        {
            var result = await _authService.GetUserByIdAsync(userId);
            if (result is not null)
                return Ok(result);
        }

        return BadRequest("Falha ao buscar usuário");
    }

    [HttpGet("GetUserRoles")]
    [Authorize]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            var result = await _authService.GetUserRolesAsync(userId);
            return Ok(result);
        }

        return BadRequest("falha na busca");
    }

    [HttpDelete("RemoveUser")]
    [Authorize]
    public async Task<IActionResult> RemoveUser(string userId)
    {
        if(!string.IsNullOrEmpty(userId) && !string.IsNullOrWhiteSpace(userId))
        {
            await _authService.RemoveUserAsync(userId);
            return Ok();
        }

        return BadRequest();
    }
}