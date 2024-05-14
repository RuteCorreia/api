using Application.DTOs.Cadastros.Piloto.Interface;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class PilotoController : ControllerBase
{
    private readonly IPilotoService _pilotoService;
    private readonly LoggedUserInfoService _loggedUserInfoService;

    public PilotoController(
        IPilotoService pilotoService, 
        LoggedUserInfoService loggedUserInfoService
    )
    {
        _pilotoService = pilotoService;
        _loggedUserInfoService = loggedUserInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<PilotoViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var pilotos = await _pilotoService.GetAllAsync(loggedUser.Item3);
            return Ok(pilotos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Piloto getAll - {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PilotoViewModel>> GetById(string id)
    {
        try
        {
            if(!string.IsNullOrEmpty(id))
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var piloto = await _pilotoService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(piloto))
                {
                    return Ok(piloto);
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Piloto getById - {ex.Message}");
        }
    }
}
