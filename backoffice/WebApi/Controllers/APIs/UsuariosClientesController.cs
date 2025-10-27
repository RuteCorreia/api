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
        public async Task<IActionResult> Get()
        {
            var idClienteClaim = User.Claims.FirstOrDefault(c => c.Type == "IdCliente")?.Value;
            int.TryParse(idClienteClaim, out var idCliente);

            var result = await _userAuthService.GetUsuariosClientesAsync(idCliente);
            return Ok(result);
        }
    }
}
