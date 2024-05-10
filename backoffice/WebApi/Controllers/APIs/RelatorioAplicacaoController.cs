using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.Interface;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
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
    public class RelatorioAplicacaoController : ControllerBase
    {
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;

        public RelatorioAplicacaoController(IRelatorioAplicacaoService relatorioAplicacaoService)
        {
            _relatorioAplicacaoService = relatorioAplicacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<RelatorioAplicacaoViewModel>>> GetAll()
        {
            try
            {
                var relatorio = await _relatorioAplicacaoService.GetAllAsync();
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Relatorio Aplicacao getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RelatorioAplicacaoViewModel>> GetById(int id)
        {
            try
            {
                var relatorio = await _relatorioAplicacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(relatorio))
                {
                    return Ok(relatorio);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Relatorio Aplicacao getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RelatorioAplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _relatorioAplicacaoService.AddAsync(obj);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Relatorio Aplicacao add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] RelatorioAplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _relatorioAplicacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _relatorioAplicacaoService.UpdateAsync(obj);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Relatorio Aplicacao update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _relatorioAplicacaoService.DeleteAsync(id);
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Relatorio Aplicacao delete - {ex.Message}");
            }
        }
    }

}
