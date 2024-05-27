using Application.DTOs.Cadastros.AplicacaoCroqui.Interface;
using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;
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
public class AplicacaoCroquiController : ControllerBase
{
    private readonly IAplicacaoCroquiService _aplicacaoCroquiService;
    private readonly ILogService _logService;

    public AplicacaoCroquiController(IAplicacaoCroquiService aplicacaoCroquiService, ILogService logService)
    {
        _aplicacaoCroquiService = aplicacaoCroquiService;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoCroquiViewModel>>> GetAll()
    {
        try
        {
            var croquis = await _aplicacaoCroquiService.GetAllAsync();
            _logService.LogInformation("Todos os registros de Croqui de Aplicação foram obtidos com sucesso.");
            return Ok(croquis);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter todos os registros de Croqui de Aplicação: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os registros de Croqui de Aplicação: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoCroquiViewModel>> GetById(int id)
    {
        try
        {
            var croqui = await _aplicacaoCroquiService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(croqui))
            {
                _logService.LogInformation($"Croqui de Aplicação com ID {id} foi obtido com sucesso.");
                return Ok(croqui);
            }

            _logService.LogWarning($"Croqui de Aplicação com ID {id} não foi encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao obter Croqui de Aplicação com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter Croqui de Aplicação com ID {id}: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoCroquiViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoCroquiService.AddAsync(obj);
                _logService.LogInformation("Novo Croqui de Aplicação adicionado com sucesso.");
                return Ok();
            }

            _logService.LogWarning("Modelo inválido ao adicionar novo Croqui de Aplicação.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar novo Croqui de Aplicação: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo Croqui de Aplicação: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCroquiViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoCroquiService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoCroquiService.UpdateAsync(obj);
                    _logService.LogInformation($"Croqui de Aplicação com ID {id} atualizado com sucesso.");
                    return Ok();
                }
                else
                {
                    _logService.LogWarning($"Croqui de Aplicação com ID {id} não encontrado para atualização.");
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _logService.LogWarning("Modelo inválido ao atualizar Croqui de Aplicação.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar Croqui de Aplicação com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Croqui de Aplicação com ID {id}: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoCroquiService.DeleteAsync(id);
                _logService.LogInformation($"Croqui de Aplicação com ID {id} foi deletado com sucesso.");
                return Ok("Deletado com sucesso");
            }

            _logService.LogWarning("Solicitação para deletar Croqui de Aplicação não pôde ser executada, ID inválido.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar Croqui de Aplicação com ID {id}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Croqui de Aplicação: {ex.Message}");
        }
    }
}
