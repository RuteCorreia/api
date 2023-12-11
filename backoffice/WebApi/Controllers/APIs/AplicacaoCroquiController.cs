using Application.DTOs.Cadastros.AplicacaoCroqui.Interface;
using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoCroquiController : ControllerBase
{
    private readonly IAplicacaoCroquiService _aplicacaoCroquiService;

    public AplicacaoCroquiController(IAplicacaoCroquiService aplicacaoCroquiService)
    {
        _aplicacaoCroquiService = aplicacaoCroquiService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoCroquiViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoCroquiService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroqui getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoCroquiViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoCroqui = await _aplicacaoCroquiService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoCroqui))
            {
                return Ok(aplicacaoCroqui);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroqui getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoCroquiViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoCroquiService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroqui add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCroquiViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoCroquiService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoCroquiService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroqui update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoCroquiService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroqui delete - {ex.Message}");
        }
    }
}
