using Application.DTOs.Cadastros.BulaAplicacao.Interface;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BulaAplicacaoController : ControllerBase
    {
        private readonly IBulaAplicacaoService _bulaAplicacaoService;

        public BulaAplicacaoController(IBulaAplicacaoService bulaAplicacaoService)
        {
            _bulaAplicacaoService = bulaAplicacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<BulaAplicacaoViewModel>>> GetAll()
        {
            try
            {
                var bulaAplicacoes = await _bulaAplicacaoService.GetAllAsync();
                return Ok(bulaAplicacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IAsyncEnumerable<BulaAplicacaoViewModel>>> GetByIdBulaAsync(int id)
        {
            try
            {
                var bulaAplicacao = await _bulaAplicacaoService.GetByIdBulaAsync(id);
                if (!ObjectNullValidation.IsObjectNull(bulaAplicacao))
                {
                    return Ok(bulaAplicacao);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] List<BulaAplicacaoViewModel> obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in obj)
                    {
                        await _bulaAplicacaoService.AddAsync(item);

                    }
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] BulaAplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _bulaAplicacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.IdBulaAplicacao = objeto.IdBulaAplicacao;

                        await _bulaAplicacaoService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _bulaAplicacaoService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante delete - {ex.Message}");
            }
        }
    }
}
