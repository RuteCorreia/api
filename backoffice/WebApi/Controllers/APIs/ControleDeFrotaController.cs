using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
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
public class ControleDeFrotaController : ControllerBase
{
    private readonly IControleDeFrotaService _controleDeFrotaService;
    private readonly ILogService _logService; // Injete o serviço de log

    public ControleDeFrotaController(IControleDeFrotaService controleDeFrotaService, ILogService logService) // Adicione o serviço de log como parâmetro do construtor
    {
        _controleDeFrotaService = controleDeFrotaService;
        _logService = logService; // Atribua o serviço de log
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ControleDeFrotaViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _controleDeFrotaService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"ControleDeFrota getAll - {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ControleDeFrotaViewModel>> GetById(int id)
    {
        try
        {
            var controleDeFrota = await _controleDeFrotaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(controleDeFrota))
            {
                return Ok(controleDeFrota);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"ControleDeFrota getById - {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _controleDeFrotaService.AddAsync(obj);
                _logService.LogInformation("ControleDeFrota adicionado com sucesso"); // Registre uma informação de log
                return Ok("Sucesso");
            }

            _logService.LogWarning("Tentativa de adição de ControleDeFrota com modelo inválido"); // Registre um aviso de log
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar ControleDeFrota: {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _controleDeFrotaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _controleDeFrotaService.UpdateAsync(obj);
                    _logService.LogInformation("ControleDeFrota atualizado com sucesso"); // Registre uma informação de log
                    return Ok("Sucesso");
                }
                else
                {
                    _logService.LogWarning("Tentativa de atualização de ControleDeFrota não encontrada"); // Registre um aviso de log
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            _logService.LogWarning("Tentativa de atualização de ControleDeFrota com modelo inválido"); // Registre um aviso de log
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar ControleDeFrota: {ex.Message}"); // Registre um erro de log
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _controleDeFrotaService.DeleteAsync(id);
                _logService.LogInformation("ControleDeFrota deletado com sucesso"); // Registre uma informação de log
                return Ok("Deletado com sucesso");
            }
            _logService.LogWarning($"Tentativa de deletar de ControleDeFrota com Id inválido: {id} "); // Registre um aviso de log

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar ControleDeFrota: {ex.Message}"); // Registre um erro de log

            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota delete - {ex.Message}");
        }
    }
}
