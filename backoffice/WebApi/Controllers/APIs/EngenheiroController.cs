using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
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
public class EngenheiroController : ControllerBase
{
    private readonly IEngenheiroService _engenheiroService;
    private readonly LoggedUserInfoService _loggedUserInfoService;


    public EngenheiroController(
        IEngenheiroService engenheiroService,
        LoggedUserInfoService loggedUserInfoService
    )
    {
        _engenheiroService = engenheiroService;
        _loggedUserInfoService = loggedUserInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<EngenheiroViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var engenheiros = await _engenheiroService.GetAllAsync(loggedUser.Item3);
            return Ok(engenheiros);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Engenheiro getAll - {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngenheiroViewModel>> GetById(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var engenheiro = await _engenheiroService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(engenheiro))
                {
                    return Ok(engenheiro);
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Engenheiro getById - {ex.Message}");
        }
    }
}
