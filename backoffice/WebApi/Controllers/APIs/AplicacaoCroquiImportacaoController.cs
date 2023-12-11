using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Interface;
using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AplicacaoCroquiImportacaoController : ControllerBase
{
    private readonly IAplicacaoCroquiImportacaoService _aplicacaoCroquiImportacaoService;

    public AplicacaoCroquiImportacaoController(IAplicacaoCroquiImportacaoService aplicacaoCroquiImportacaoService)
    {
        _aplicacaoCroquiImportacaoService = aplicacaoCroquiImportacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<AplicacaoCroquiImportacaoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _aplicacaoCroquiImportacaoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroquiImportacao getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AplicacaoCroquiImportacaoViewModel>> GetById(int id)
    {
        try
        {
            var aplicacaoCroquiImportacao = await _aplicacaoCroquiImportacaoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(aplicacaoCroquiImportacao))
            {
                return Ok(aplicacaoCroquiImportacao);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroquiImportacao getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AplicacaoCroquiImportacaoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _aplicacaoCroquiImportacaoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroquiImportacao add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCroquiImportacaoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _aplicacaoCroquiImportacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.Id = objeto.Id;

                    await _aplicacaoCroquiImportacaoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroquiImportacao update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _aplicacaoCroquiImportacaoService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"AplicacaoCroquiImportacao delete - {ex.Message}");
        }
    }
}
