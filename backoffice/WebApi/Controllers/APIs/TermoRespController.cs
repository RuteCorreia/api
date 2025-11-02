using Application.DTOs.Users.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class TermoRespController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;

    public TermoRespController(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    [HttpPost("salvarFlagTermo")]
    public async Task<IActionResult> SalvarFlagTermo([FromBody] FlagTermoRespRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var (ok, msg) = await _userAuthService.SaveUserFlagTermoRespAsync(userId, request.FlagTermoResp);
        if (!ok)
            return BadRequest(msg);

        return Ok(new { success = true, message = msg });
    }
}
