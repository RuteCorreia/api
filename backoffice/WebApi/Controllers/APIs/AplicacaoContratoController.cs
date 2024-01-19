using Application.DTOs.Cadastros.AplicacaoContrato.Interface;
using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;
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
public class AplicacaoContratoController : ControllerBase
{
    private readonly IAplicacaoContratoService _aplicacaoContratoService;

    public AplicacaoContratoController(IAplicacaoContratoService aplicacaoContratoService)
    {
        _aplicacaoContratoService = aplicacaoContratoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoContratoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoContratoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoContrato getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoContratoViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoContrato = await _aplicacaoContratoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoContrato))
            {
                return Ok(aplicacaoContrato);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoContrato getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoContratoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoContratoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoContrato add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoContratoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoContratoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoContratoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoContrato update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoContratoService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoContrato delete - {ex.Message}");
        }
    }
}
