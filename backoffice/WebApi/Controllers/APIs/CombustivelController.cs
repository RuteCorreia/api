using Application.DTOs.Cadastros.Combustivel.Interface;
using Application.DTOs.Cadastros.Combustivel.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CombustivelController : ControllerBase
{
    private readonly ICombustivelService _combustivelService;
    private readonly ILogService _loggerService;

    public CombustivelController(ICombustivelService combustivelService, ILogService loggerService)
    {
        _combustivelService = combustivelService;
        _loggerService = loggerService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CombustivelViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _combustivelService.GetAllAsync();
            _loggerService.LogInformation("Todos os registros de combustível foram recuperados com sucesso.");
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar todos os registros de combustível: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de combustível: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CombustivelViewModel>> GetById(int id)
    {
        try
        {
            var combustivel = await _combustivelService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(combustivel))
            {
                _loggerService.LogInformation($"Registro de combustível com ID {id} foi recuperado com sucesso.");
                return Ok(combustivel);
            }

            _loggerService.LogWarning($"Registro de combustível com ID {id} não encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao buscar registro de combustível com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar registro de combustível com ID {id}: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CombustivelViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _combustivelService.AddAsync(obj);
                _loggerService.LogInformation("Novo registro de combustível adicionado com sucesso.");
                return Ok("Sucesso");
            }

            _loggerService.LogWarning("Modelo inválido ao adicionar novo registro de combustível.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao adicionar novo registro de combustível: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo registro de combustível: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CombustivelViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _combustivelService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _combustivelService.UpdateAsync(obj);
                    _loggerService.LogInformation($"Registro de combustível com ID {id} atualizado com sucesso.");
                    return Ok("Sucesso");
                }
                else
                {
                    _loggerService.LogWarning($"Registro de combustível com ID {id} não encontrado.");
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _loggerService.LogWarning("Modelo inválido ao atualizar registro de combustível.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao atualizar registro de combustível com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar registro de combustível com ID {id}: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _combustivelService.DeleteAsync(id);
                _loggerService.LogInformation($"Registro de combustível com ID {id} deletado com sucesso.");
                return Ok("Deletado com sucesso");
            }

            _loggerService.LogWarning("ID inválido ao tentar deletar registro de combustível.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _loggerService.LogError(ex, $"Erro ao deletar registro de combustível com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Combustivel delete - {ex.Message}");
        }
    }
}
