using Application.DTOs.Cadastros.AplicacaoCaracteristicas.Interface;
using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoCaracteristicasController : ControllerBase
{
    private readonly IAplicacaoCaracteristicasService _aplicacaoCaracteristicasService;

    public AplicacaoCaracteristicasController(IAplicacaoCaracteristicasService aplicacaoCaracteristicasService)
    {
        _aplicacaoCaracteristicasService = aplicacaoCaracteristicasService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoCaracteristicasViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoCaracteristicasService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCaracteristicas getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoCaracteristicasViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoCaracteristicas = await _aplicacaoCaracteristicasService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoCaracteristicas))
            {
                return Ok(aplicacaoCaracteristicas);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCaracteristicas getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoCaracteristicasViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoCaracteristicasService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCaracteristicas add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCaracteristicasViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoCaracteristicasService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoCaracteristicasService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCaracteristicas update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoCaracteristicasService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCaracteristicas delete - {ex.Message}");
        }
    }
}
