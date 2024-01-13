using Application.DTOs.Cadastros.PlanoDeContrato.Interface;
using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;
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
public class PlanoDeContratoController : ControllerBase
{
    private readonly IPlanoDeContratoService _planoDeContratoService;

    public PlanoDeContratoController(IPlanoDeContratoService planoDeContratoService)
    {
        _planoDeContratoService = planoDeContratoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<PlanoDeContratoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _planoDeContratoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"PlanoDeContrato getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PlanoDeContratoViewModel>> GetById(int id)
    {
        try
        {
            var planoDeContrato = await _planoDeContratoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(planoDeContrato))
            {
                return Ok(planoDeContrato);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"PlanoDeContrato getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] PlanoDeContratoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _planoDeContratoService.AddAsync(obj);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"PlanoDeContrato add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PlanoDeContratoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _planoDeContratoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.IdPlano = objeto.IdPlano;

                    await _planoDeContratoService.UpdateAsync(obj);
                    return Ok();
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"PlanoDeContrato update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _planoDeContratoService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"PlanoDeContrato delete - {ex.Message}");
        }
    }
}
