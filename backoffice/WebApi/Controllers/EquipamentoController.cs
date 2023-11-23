using Application.DTOs.Cadastros.Equipamento.Interface;
using Application.DTOs.Cadastros.Equipamento.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class EquipamentoController : ControllerBase
{
    private readonly IEquipamentoService _equipamentoService;

    public EquipamentoController(IEquipamentoService equipamentoService)
    {
        _equipamentoService = equipamentoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<EquipamentoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _equipamentoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Equipamento getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EquipamentoViewModel>> GetById(int id)
    {
        try
        {
            var equipamento = await _equipamentoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(equipamento))
            {
                return Ok(equipamento);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Equipamento getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] EquipamentoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _equipamentoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Equipamento add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] EquipamentoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _equipamentoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _equipamentoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Equipamento update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _equipamentoService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Equipamento delete - {ex.Message}");
        }
    }
}
