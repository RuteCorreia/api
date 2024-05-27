using Application.DTOs.Cadastros.Veiculante.Interface;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
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
public class VeiculanteController : ControllerBase
{
    private readonly IVeiculanteService _veiculanteService;
    private readonly ILogService _logService;

    public VeiculanteController(IVeiculanteService veiculanteService, ILogService logService)
    {
        _veiculanteService = veiculanteService;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<VeiculanteViewModel>>> GetAll()
    {
        try
        {
            var veiculantes = await _veiculanteService.GetAllAsync();
            _logService.LogInformation("Todos os veiculantes foram recuperados com sucesso.");
            return Ok(veiculantes);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar todos os veiculantes: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os veiculantes: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VeiculanteViewModel>> GetById(int id)
    {
        try
        {
            var veiculante = await _veiculanteService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(veiculante))
            {
                _logService.LogInformation("Veiculante recuperado com sucesso.");
                return Ok(veiculante);
            }

            _logService.LogWarning("Veiculante não encontrado.");
            return StatusCode(StatusCodes.Status404NotFound, "Veiculante não encontrado");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar veiculante pelo ID: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar veiculante pelo ID: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] VeiculanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _veiculanteService.AddAsync(obj);
                _logService.LogInformation("Novo veiculante adicionado com sucesso.");
                return Ok();
            }

            _logService.LogWarning("Modelo inválido ao adicionar novo veiculante.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar novo veiculante: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo veiculante: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VeiculanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _veiculanteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.IdVeiculante = objeto.IdVeiculante;

                    await _veiculanteService.UpdateAsync(obj);
                    _logService.LogInformation("Veiculante atualizado com sucesso.");
                    return Ok();
                }
                else
                {
                    _logService.LogWarning("Veiculante não encontrado para atualização.");
                    return StatusCode(StatusCodes.Status404NotFound, "Veiculante não encontrado");
                }
            }

            _logService.LogWarning("Modelo inválido ao atualizar veiculante.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar veiculante: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar veiculante: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _veiculanteService.DeleteAsync(id);
                _logService.LogInformation("Veiculante deletado com sucesso.");
                return Ok();
            }

            _logService.LogWarning("Solicitação inválida para deletar veiculante.");
            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar veiculante: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar veiculante: {ex.Message}");
        }
    }
}
