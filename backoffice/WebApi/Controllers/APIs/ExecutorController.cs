using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Executor.ViewModel;
using Application.DTOs.Log.Interface;
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
public class ExecutorController : ControllerBase
{
    private readonly IExecutorService _executorService;
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly ILogService _logService;

    public ExecutorController(
        IExecutorService executorService,
         LoggedUserInfoService loggedUserInfoService,
         ILogService logService

    )
    {
        _executorService = executorService;
        _loggedUserInfoService = loggedUserInfoService;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ExecutorViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var executores = await _executorService.GetAllAsync(loggedUser.Item3);
            _logService.LogInformation("Lista de todos os executores obtida com sucesso.");
            return Ok(executores);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter todos os executores: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os executores: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExecutorViewModel>> GetById(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var executor = await _executorService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(executor))
                {
                    _logService.LogInformation($"Detalhes do executor com ID {id} obtidos com sucesso.");
                    return Ok(executor);
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter detalhes do executor com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes do executor com ID {id}: {ex.Message}");
        }
    }
}