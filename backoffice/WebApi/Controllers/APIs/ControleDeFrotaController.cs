using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ControleDeFrotaController : ControllerBase
{
    private readonly IControleDeFrotaService _controleDeFrotaService;

    public ControleDeFrotaController(IControleDeFrotaService controleDeFrotaService)
    {
        _controleDeFrotaService = controleDeFrotaService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ControleDeFrotaViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _controleDeFrotaService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ControleDeFrotaViewModel>> GetById(int id)
    {
        try
        {
            var controleDeFrota = await _controleDeFrotaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(controleDeFrota))
            {
                return Ok(controleDeFrota);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _controleDeFrotaService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ControleDeFrotaViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _controleDeFrotaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _controleDeFrotaService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _controleDeFrotaService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ControleDeFrota delete - {ex.Message}");
        }
    }
}
