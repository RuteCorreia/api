using Application.DTOs.Cadastros.AplicacaoLog.Interface;
using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoLogController : ControllerBase
{
    private readonly IAplicacaoLogService _aplicacaoLogService;

    public AplicacaoLogController(IAplicacaoLogService aplicacaoLogService)
    {
        _aplicacaoLogService = aplicacaoLogService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoLogViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoLogService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoLog getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoLogViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoLog = await _aplicacaoLogService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoLog))
            {
                return Ok(aplicacaoLog);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoLog getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoLogViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoLogService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoLog add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoLogViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoLogService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoLogService.UpdateAsync(obj);
                    return Ok("Sucesso");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoLog update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoLogService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoLog delete - {ex.Message}");
        }
    }
}
