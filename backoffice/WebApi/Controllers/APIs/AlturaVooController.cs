using Application.DTOs.Cadastros.AlturaVoo.Interface;
using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AlturaVooController : ControllerBase
{
    private readonly IAlturaVooService _alturaVooService;

    public AlturaVooController(IAlturaVooService alturaVooService)
    {
        _alturaVooService = alturaVooService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AlturaVooViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _alturaVooService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlturaVooViewModel>> GetById(int id)
    {
        try
        {
            var alturaVoo = await _alturaVooService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(alturaVoo))
            {
                return Ok(alturaVoo);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AlturaVooViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _alturaVooService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AlturaVooViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _alturaVooService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _alturaVooService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _alturaVooService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo delete - {ex.Message}");
        }
    }
}
