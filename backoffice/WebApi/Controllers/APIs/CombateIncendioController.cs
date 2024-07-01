using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
//[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CombateIncendioController : ControllerBase
{
    private readonly ICombateIncendioService _combateIncendioService;
    private readonly ILogService _loggerService;

    public CombateIncendioController(ICombateIncendioService combateIncendioService, ILogService loggerService)
    {
        _combateIncendioService = combateIncendioService;
        _loggerService = loggerService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CombateIncendioViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _combateIncendioService.GetAllAsync();
            _loggerService.LogInformation("Todos os registros de Combate a Incêndio foram recuperados com sucesso.");
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Combate a Incêndio: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CombateIncendioViewModel>> GetById(int id)
    {
        try
        {
            var combateIncendio = await _combateIncendioService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(combateIncendio))
            {
                _loggerService.LogInformation($"Registro de Combate a Incêndio com ID {id} foi recuperado com sucesso.");
                return Ok(combateIncendio);
            }

            _loggerService.LogWarning($"Registro de Combate a Incêndio com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar registro de Combate a Incêndio com ID {id}: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CombateIncendioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _combateIncendioService.AddAsync(obj);
                _loggerService.LogInformation("Novo registro de Combate a Incêndio adicionado com sucesso.");
                return Ok("Sucesso");
            }

            _loggerService.LogWarning("Modelo inválido ao adicionar novo registro de Combate a Incêndio.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao adicionar novo registro de Combate a Incêndio: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo registro de Combate a Incêndio: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CombateIncendioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _combateIncendioService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;
                    await _combateIncendioService.UpdateAsync(obj);
                    _loggerService.LogInformation($"Registro de Combate a Incêndio com ID {id} atualizado com sucesso.");
                    return Ok("Sucesso");
                }
                else
                {
                    _loggerService.LogWarning($"Registro de Combate a Incêndio com ID {id} não encontrado.");
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _loggerService.LogWarning("Modelo inválido ao atualizar registro de Combate a Incêndio.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao atualizar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _combateIncendioService.DeleteAsync(id);
                _loggerService.LogInformation($"Registro de Combate a Incêndio com ID {id} deletado com sucesso.");
                return Ok("Deletado com sucesso");
            }
            _loggerService.LogWarning($"Registro de Combate a Incêndio com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao deletar registro de Combate a Incêndio com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendio delete - {ex.Message}");
        }
    }
}
