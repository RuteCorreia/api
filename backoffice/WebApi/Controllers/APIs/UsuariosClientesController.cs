using Application.DTOs.Users.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;
using System.Security.Claims;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosClientesController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IUserAuthService _userAuthService;

        public UsuariosClientesController(LoggedUserInfoService loggedUserInfoService, IUserAuthService userAuthService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _userAuthService = userAuthService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var idEmpresaClaim = User.Claims.FirstOrDefault(c => c.Type == "IdEmpresa")?.Value;
            int idEmpresa;

            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

            if (!int.TryParse(idEmpresaClaim, out idEmpresa))
            {
                if (!int.TryParse(loggedUser.Item3, out idEmpresa))
                    return BadRequest("IdEmpresa inválido no token.");
            }

            // Recupera userId do claim (NameIdentifier) da mesma forma que IdEmpresa
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = !string.IsNullOrEmpty(userIdClaim) ? userIdClaim : loggedUser.Item1;

            var result = await _userAuthService.GetUsuariosClientesAsync(idEmpresa, userId ?? string.Empty);
            return Ok(result);
        }
    }
}
