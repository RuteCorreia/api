using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
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
public class AplicacaoRecomendacoesTecnicasController : ControllerBase
{
    private readonly IAplicacaoRecomendacoesTecnicasService _aplicacaoRecomendacoesTecnicasService;

    public AplicacaoRecomendacoesTecnicasController(IAplicacaoRecomendacoesTecnicasService aplicacaoRecomendacoesTecnicasService)
    {
        _aplicacaoRecomendacoesTecnicasService = aplicacaoRecomendacoesTecnicasService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoRecomendacoesTecnicasViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoRecomendacoesTecnicasService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRecomendacoesTecnicas getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoRecomendacoesTecnicasViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoRecomendacoesTecnicas = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoRecomendacoesTecnicas))
            {
                return Ok(aplicacaoRecomendacoesTecnicas);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRecomendacoesTecnicas getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoRecomendacoesTecnicasViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoRecomendacoesTecnicasService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRecomendacoesTecnicas add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRecomendacoesTecnicasViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoRecomendacoesTecnicasService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRecomendacoesTecnicas update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoRecomendacoesTecnicasService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoRecomendacoesTecnicas delete - {ex.Message}");
        }
    }
}
