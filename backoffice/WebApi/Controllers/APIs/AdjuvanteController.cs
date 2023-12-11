using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AdjuvanteController : ControllerBase
{
    private readonly IAdjuvanteService _adjuvanteService;

    public AdjuvanteController(IAdjuvanteService adjuvanteService)
    {
        _adjuvanteService = adjuvanteService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AdjuvanteViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _adjuvanteService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AdjuvanteViewModel>> GetById(int id)
    {
        try
        {
            var adjuvante = await _adjuvanteService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(adjuvante))
            {
                return Ok(adjuvante);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AdjuvanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _adjuvanteService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AdjuvanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _adjuvanteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _adjuvanteService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _adjuvanteService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante delete - {ex.Message}");
        }
    }
}
