using Application.DTOs.Users.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

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
        public async Task<IActionResult> Get(int idCliente)
        {
            var logged = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            // if (string.IsNullOrEmpty(logged.Item1)) return Unauthorized();

            int? idEmpresa = null;
            if (int.TryParse(logged.Item3, out var parsed)) idEmpresa = parsed;

            var result = await _userAuthService.GetUsuariosClientesAsync(idCliente, idEmpresa);
            return Ok(result);
        }
    }
}
