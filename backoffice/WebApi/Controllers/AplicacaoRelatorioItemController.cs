using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoRelatorioItemController : ControllerBase
{
    private readonly IAplicacaoRelatorioItemService _aplicacaoRelatorioItemService;

    public AplicacaoRelatorioItemController(IAplicacaoRelatorioItemService aplicacaoRelatorioItemService)
    {
        _aplicacaoRelatorioItemService = aplicacaoRelatorioItemService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoRelatorioItemViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoRelatorioItemService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorioItem getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoRelatorioItemViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoRelatorioItem = await _aplicacaoRelatorioItemService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoRelatorioItem))
            {
                return Ok(aplicacaoRelatorioItem);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorioItem getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoRelatorioItemViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoRelatorioItemService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorioItem add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRelatorioItemViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = _aplicacaoRelatorioItemService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _aplicacaoRelatorioItemService.AddAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorioItem update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoRelatorioItemService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorioItem delete - {ex.Message}");
        }
    }
}
