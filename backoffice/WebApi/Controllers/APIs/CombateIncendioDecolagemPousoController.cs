using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
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
public class CombateIncendioDecolagemPousoController : ControllerBase
{
    private readonly ICombateIncendioDecolagemPousoService _combateIncendioDecolagemPousoService;
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly ILogService _loggerService;

    public CombateIncendioDecolagemPousoController(
        ICombateIncendioDecolagemPousoService combateIncendioDecolagemPousoService,
        LoggedUserInfoService loggedUserInfoService,
        ILogService loggerService)
    {
        _combateIncendioDecolagemPousoService = combateIncendioDecolagemPousoService;
        _loggedUserInfoService = loggedUserInfoService;
        _loggerService = loggerService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CombateIncendioDecolagemPousoViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var combustiveis = await _combateIncendioDecolagemPousoService.GetAllAsync(loggedUser.Item3);
            _loggerService.LogInformation("Todos os registros de Combate a Incêndio em Decolagem e Pouso foram recuperados com sucesso.");
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
        }
    }

    [HttpGet("GetByCombateIncendioId/{combateIncendioId}")]
    public async Task<ActionResult<IAsyncEnumerable<CombateIncendioDecolagemPousoViewModel>>> GetByCombateIncendioId(int combateIncendioId)
    {
        try
        {
            var result = await _combateIncendioDecolagemPousoService.GetByCombateIncendioIdAsync(combateIncendioId);
            _loggerService.LogInformation("Todos os registros de Combate a Incêndio em Decolagem e Pouso foram recuperados com sucesso.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CombateIncendioDecolagemPousoViewModel>> GetById(int id)
    {
        try
        {
            var combateIncendioDecolagemPouso = await _combateIncendioDecolagemPousoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(combateIncendioDecolagemPouso))
            {
                _loggerService.LogInformation($"Registro de Combate a Incêndio em Decolagem e Pouso com ID {id} foi recuperado com sucesso.");
                return Ok(combateIncendioDecolagemPouso);
            }

            _loggerService.LogWarning($"Registro de Combate a Incêndio em Decolagem e Pouso com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar registro de Combate a Incêndio em Decolagem e Pouso com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar registro de Combate a Incêndio em Decolagem e Pouso com ID {id}: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CombateIncendioDecolagemPousoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var id = await _combateIncendioDecolagemPousoService.AddAsync(obj, loggedUser.Item3);
                _loggerService.LogInformation("Novo registro de Combate a Incêndio em Decolagem e Pouso adicionado com sucesso.");
                return Ok(id);
            }

            _loggerService.LogWarning("Modelo inválido ao adicionar novo registro de Combate a Incêndio em Decolagem e Pouso.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao adicionar novo registro de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo registro de Combate a Incêndio em Decolagem e Pouso: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] CombateIncendioDecolagemPousoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _combateIncendioDecolagemPousoService.GetByIdAsync(obj.Id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    var result = await _combateIncendioDecolagemPousoService.UpdateAsync(obj);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _loggerService.LogWarning("Modelo inválido ao atualizar registro de Combate a Incêndio em Decolagem e Pouso.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar registro de Combate a Incêndio em Decolagem e Pouso com ID");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _combateIncendioDecolagemPousoService.DeleteAsync(id);
                _loggerService.LogInformation($"Registro de Combate a Incêndio em Decolagem e Pouso com ID {id} deletado com sucesso.");
                return Ok("Deletado com sucesso");
            }

            _loggerService.LogWarning($"Registro de Combate a Incêndio em Decolagem e Pouso com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao deletar registro de Combate a Incêndio em Decolagem e Pouso com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso delete - {ex.Message}");
        }
    }
}
