using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.Aeronave.ViewModel;
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
public class AeronaveController : ControllerBase
{
    private readonly IAeronaveService _aeronaveService;

    public AeronaveController(IAeronaveService aeronaveService)
    {
        _aeronaveService = aeronaveService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AeronaveViewModel>>> GetAll()
    {
        try
        {
            var aeronaves = await _aeronaveService.GetAllAsync();
            return Ok(aeronaves);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AeronaveViewModel>> GetById(int id)
    {
        try
        {
            var aeronave = await _aeronaveService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aeronave))
            {
                return Ok(aeronave);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AeronaveViewModel obj)
    {
        try
        {
            var aeronaves = await _aeronaveService.GetAllAsync();
            if(!aeronaves.Any(x => string.Equals(x.Prefixo?.ToLower(), obj.Prefixo?.ToLower())))
            {
                if (ModelState.IsValid)
                {
                    await _aeronaveService.AddAsync(obj);
                    return Ok();
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Prefixo não pode ser duplicado (já existe outra aeronave com esse prefixo)");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AeronaveViewModel obj)
    {
        try
        {
            var aeronaves = await _aeronaveService.GetAllAsync();
            if (!aeronaves.Any(x => string.Equals(x.Prefixo?.ToLower(), obj.Prefixo?.ToLower()) && x.Id != obj.Id))
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aeronaveService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _aeronaveService.UpdateAsync(obj);
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Prefixo não pode ser duplicado (já existe outra aeronave com esse prefixo)");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aeronaveService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave delete - {ex.Message}");
        }
    }
}
