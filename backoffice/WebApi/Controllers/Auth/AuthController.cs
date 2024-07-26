using Application.DTOs.Email.Interface;
using Application.DTOs.Email.ViewModel;
using Application.DTOs.Users.Interface;
using Application.DTOs.Users.ViewModel;
using Domain.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.Auth;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AuthController : ControllerBase
{
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IUserAuthService _userAuthService;
    private readonly IUserAuthService _authService;
    private readonly IEmailService _emailService;
    public AuthController(
        LoggedUserInfoService loggedUserInfoService,
        IUserAuthService userAuthService,
        IUserAuthService authService,  
        IEmailService emailService )
    {
        _loggedUserInfoService = loggedUserInfoService;
        _userAuthService = userAuthService;
        _emailService = emailService;
        _authService = authService;
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
    public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordViewModel model)
    {
        var resultError = new StringBuilder().Append("Não foi possível alterar a senha");
        if (ModelState.IsValid)
        {
            var (success, message) = await _authService.ChangeUserPasswordAsync(model);
            if (success)
                return Ok();

            resultError.Clear();
            resultError.Append(message);
        }

        return BadRequest(resultError.ToString());
    }

    [HttpPost("sendEmailPassword")]
    public async Task<IActionResult> SendEmailChangePassword([FromBody] UserSendEmailResetPasswordViewModel model)
    {
        var resultError = new StringBuilder().Append("Não foi possível enviar o e-mail, tente novamente");
        if (ModelState.IsValid) 
        {
            var (sucess, message) = await _emailService.GeneratePasswordResetTokenAsync(model.Email);
            if(sucess) 
            {
                var emailContent = new EmailViewModel
                {
                    Recipient = model.Email,
                    Title = "Recuperação de Senha",
                    Body = $"Você solicitou a recuperação de senha. Clique no link abaixo para criar uma nova senha.",
                    Link = $"https://flytec-web.azurewebsites.net/trocarSenha?token={HttpUtility.UrlEncode(message)}&email={HttpUtility.UrlEncode(model.Email)}",
                    LinkText = "Criar Nova Senha"
                };

                try
                {
                    await _emailService.SendMailAsync(emailContent);
                    return Ok(new { message = "E-mail de recuperação de senha enviado com sucesso." });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao enviar e-mail de recuperação de senha: {ex.Message}");
                }
            }
            else
            {
                resultError.Clear();
                resultError.Append(message);
            }
        }
        return BadRequest(resultError.ToString());
    }


    [HttpPatch("saveSignature")]
    [Authorize]
    public async Task<IActionResult> SaveUserSignature([FromBody] UserSaveSignatureViewModel obj)
    {
        var resultError = new StringBuilder().Append("Campos inválidos");
        if (ModelState.IsValid)
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (!string.IsNullOrEmpty(loggedUser.Item1))
            {
                var result = await _authService.SaveUserSignatureAsync(obj, loggedUser.Item1);
                if(result.Item1)
                    return Ok();

                resultError.Clear();
                resultError.Append(result.Item2);
            }
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

    [HttpPost("getUserSignature")]
    [Authorize]
    public async Task<IActionResult> GetUserSignature([FromBody] string userId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            var result = await _authService.GetUserSignatureAsync(userId);
            if (result is not null) return Ok(result);
        }

        return BadRequest("Falha na busca da assinatura");
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

    [HttpGet("getProfile")]
    public async Task<IActionResult> GetProfile()
    {
        var identity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;

        if (identity != null)
        {
            var userId = identity.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var userProfileViewModel = await _userAuthService.GetUserProfileAsync(userId);
                if (userProfileViewModel != null)
                {
                    return Ok(userProfileViewModel);
                }
            }
        }

        return Unauthorized();
    }
}