using Application.DTOs.Cadastros.AplicacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoAreaTratadaController : ControllerBase
{
    private readonly IAplicacaoAreaTratadaService _aplicacaoAreaTratadaService;

    public AplicacaoAreaTratadaController(IAplicacaoAreaTratadaService aplicacaoAreaTratadaService)
    {
        _aplicacaoAreaTratadaService = aplicacaoAreaTratadaService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoAreaTratadaViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoAreaTratadaService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoAreaTratada getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoAreaTratadaViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoAreaTratada = await _aplicacaoAreaTratadaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoAreaTratada))
            {
                return Ok(aplicacaoAreaTratada);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoAreaTratada getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoAreaTratadaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoAreaTratadaService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoAreaTratada add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoAreaTratadaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoAreaTratadaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoAreaTratadaService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoAreaTratada update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoAreaTratadaService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoAreaTratada delete - {ex.Message}");
        }
    }
}
