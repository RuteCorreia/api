using Application.DTOs.Cadastros.Cultura.Interface;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CulturaController : ControllerBase
{
    private readonly ICulturaService _culturaService;

    public CulturaController(ICulturaService culturaService)
    {
        _culturaService = culturaService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CulturaViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _culturaService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CulturaViewModel>> GetById(int id)
    {
        try
        {
            var cultura = await _culturaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(cultura))
            {
                return Ok(cultura);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CulturaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _culturaService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CulturaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = _culturaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _culturaService.AddAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _culturaService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura delete - {ex.Message}");
        }
    }
}
