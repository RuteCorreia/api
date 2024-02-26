using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.BulaAplicacao.Interface;
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
public class BulaController : ControllerBase
{
    private readonly IBulaService _bulaService;
    private readonly IBulaAplicacaoService _bulaAplicacaoService;

    public BulaController(IBulaService bulaService, IBulaAplicacaoService bulaAplicacaoService)
    {
        _bulaService = bulaService;
        _bulaAplicacaoService = bulaAplicacaoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<BulaViewModel>>> GetAll()
    {
        try
        {
            var bulas = await _bulaService.GetAllAsync();
            return Ok(bulas);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Bula getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BulaViewModel>> GetById(int id)
    {
        try
        {
            var bula = await _bulaService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(bula))
            {
                return Ok(bula);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Bula getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] BulaViewModel obj)
    {
        try
        {
            var verificaSeBulaExistePeloNome = _bulaService.GetByName(obj.NomeProduto).Result;
            if(verificaSeBulaExistePeloNome != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma bula com esse nome!");
            }
            if (ModelState.IsValid)
            {
                obj.IdAlvoBiologico = 3;
                obj.IdCultura = 2;
                await _bulaService.AddAsync(obj);
                var lista = await _bulaService.GetAllAsync();
                var ultimoCriado = lista.LastOrDefault();
                foreach (var item in obj.BulaAplicacoes)
                {
                    item.IdBula = ultimoCriado.IdBula;
                    await _bulaAplicacaoService.AddAsync(item);

                }
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Bula add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] BulaViewModel obj)
    {
        try
        {
            var verificaSeBulaExistePeloNome = _bulaService.GetByName(obj.NomeProduto).Result;
            if (verificaSeBulaExistePeloNome != null && verificaSeBulaExistePeloNome.IdBula != obj.IdBula)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma bula com esse nome!");
            }
            if (ModelState.IsValid)
            {
                var objeto = await _bulaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.IdBula = objeto.IdBula;

                    await _bulaService.UpdateAsync(obj);

                    foreach (var item in obj.BulaAplicacoes)
                    {
                        item.IdBula = id;
                        item.IdBulaAplicacao = 0;
                        await _bulaAplicacaoService.AddAsync(item);

                    }
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Bula update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _bulaService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Bula delete - {ex.Message}");
        }
    }
}
