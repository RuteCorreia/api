using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
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
public class CombateIncendioDecolagemPousoController : ControllerBase
{
    private readonly ICombateIncendioDecolagemPousoService _combateIncendioDecolagemPousoService;

    public CombateIncendioDecolagemPousoController(ICombateIncendioDecolagemPousoService combateIncendioDecolagemPousoService)
    {
        _combateIncendioDecolagemPousoService = combateIncendioDecolagemPousoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CombateIncendioDecolagemPousoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _combateIncendioDecolagemPousoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso getAll - {ex.Message}");
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
                return Ok(combateIncendioDecolagemPouso);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CombateIncendioDecolagemPousoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _combateIncendioDecolagemPousoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CombateIncendioDecolagemPousoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _combateIncendioDecolagemPousoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _combateIncendioDecolagemPousoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso update - {ex.Message}");
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
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CombateIncendioDecolagemPouso delete - {ex.Message}");
        }
    }
}
