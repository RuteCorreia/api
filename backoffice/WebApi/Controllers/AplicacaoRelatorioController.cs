using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoRelatorioController : ControllerBase
{
    private readonly IAplicacaoRelatorioService _aplicacaoRelatorioService;

    public AplicacaoRelatorioController(IAplicacaoRelatorioService aplicacaoRelatorioService)
    {
        _aplicacaoRelatorioService = aplicacaoRelatorioService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoRelatorioViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoRelatorioService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorio getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoRelatorioViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoRelatorio = await _aplicacaoRelatorioService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoRelatorio))
            {
                return Ok(aplicacaoRelatorio);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorio getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoRelatorioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoRelatorioService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorio add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRelatorioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoRelatorioService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoRelatorioService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorio update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoRelatorioService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRelatorio delete - {ex.Message}");
        }
    }
}
