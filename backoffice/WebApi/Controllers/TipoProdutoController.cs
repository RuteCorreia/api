using Application.DTOs.Cadastros.Tipo_Produto.Interface;
using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class TipoProdutoController : ControllerBase
{
    private readonly ITipoProdutoService _tipoProdutoService;

    public TipoProdutoController(ITipoProdutoService tipoProdutoService)
    {
        _tipoProdutoService = tipoProdutoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<TipoProdutoViewModel>>> GetAll()
    {
        try
        {
            var combustiveis = await _tipoProdutoService.GetAllAsync();
            return Ok(combustiveis);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"TipoProduto getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoProdutoViewModel>> GetById(int id)
    {
        try
        {
            var tipoProduto = await _tipoProdutoService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(tipoProduto))
            {
                return Ok(tipoProduto);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"TipoProduto getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] TipoProdutoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _tipoProdutoService.AddAsync(obj);
                return Ok("Sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"TipoProduto add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] TipoProdutoViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = _tipoProdutoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    await _tipoProdutoService.AddAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"TipoProduto update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _tipoProdutoService.DeleteAsync(id);
                return Ok("Deletado com sucesso");
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"TipoProduto delete - {ex.Message}");
        }
    }
}
