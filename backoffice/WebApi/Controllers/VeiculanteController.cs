using Application.DTOs.Cadastros.Veiculante.Interface;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class VeiculanteController : ControllerBase
{
    private readonly IVeiculanteService _veiculanteService;

    public VeiculanteController(IVeiculanteService veiculanteService)
    {
        _veiculanteService = veiculanteService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<VeiculanteViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _veiculanteService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculante getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VeiculanteViewModel>> GetById(int id)
    {
        try
        {
            var veiculante = await _veiculanteService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(veiculante))
            {
                return Ok(veiculante);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculante getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] VeiculanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _veiculanteService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculante add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VeiculanteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _veiculanteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.IdVeiculante = objeto.IdVeiculante;

                    await _veiculanteService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculante update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _veiculanteService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculante delete - {ex.Message}");
        }
    }
}
