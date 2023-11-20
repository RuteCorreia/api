using Application.DTOs.Cadastros.Precificacao.Interface;
using Application.DTOs.Cadastros.Precificacao.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class PrecificacaoController : ControllerBase
{
    private readonly IPrecificacaoService _precificacaoService;

    public PrecificacaoController(IPrecificacaoService precificacaoService)
    {
        _precificacaoService = precificacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<PrecificacaoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _precificacaoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Precificacao getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PrecificacaoViewModel>> GetById(int id)
    {
        try
        {
            var precificacao = await _precificacaoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(precificacao))
            {
                return Ok(precificacao);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Precificacao getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] PrecificacaoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _precificacaoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Precificacao add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] PrecificacaoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = _precificacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _precificacaoService.AddAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Precificacao update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _precificacaoService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Precificacao delete - {ex.Message}");
        }
    }
}
