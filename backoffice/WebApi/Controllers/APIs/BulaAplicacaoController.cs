using Application.DTOs.Cadastros.BulaAplicacao.Interface;
using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BulaAplicacaoController : ControllerBase
    {
        private readonly IBulaAplicacaoService _bulaAplicacaoService;
        private readonly ILogService _loggerService;

        public BulaAplicacaoController(IBulaAplicacaoService bulaAplicacaoService, ILogService loggerService)
        {
            _bulaAplicacaoService = bulaAplicacaoService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<BulaAplicacaoViewModel>>> GetAll()
        {
            try
            {
                var bulaAplicacoes = await _bulaAplicacaoService.GetAllAsync();
                _loggerService.LogInformation("Todos os registros de Bula de Aplicação foram recuperados com sucesso.");
                return Ok(bulaAplicacoes);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os registros de Bula de Aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de Bula de Aplicação: {ex.Message}");
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
                    _loggerService.LogInformation($"A Bula de Aplicação com ID {id} foi recuperada com sucesso.");
                    return Ok(bulaAplicacao);
                }

                _loggerService.LogWarning($"A Bula de Aplicação com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar a Bula de Aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar a Bula de Aplicação: {ex.Message}");
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
                    _loggerService.LogInformation("Registros de Bula de Aplicação adicionados com sucesso.");
                    return Ok();
                }

                _loggerService.LogWarning("Tentativa de adicionar registros de Bula de Aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar registros de Bula de Aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar registros de Bula de Aplicação: {ex.Message}");
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
                        _loggerService.LogInformation($"Bula de Aplicação com ID {id} atualizada com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _loggerService.LogWarning($"A Bula de Aplicação com ID {id} não foi encontrada.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Tentativa de atualizar uma Bula de Aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar Bula de Aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Bula de Aplicação: {ex.Message}");
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
                    _loggerService.LogInformation($"Bula de Aplicação com ID {id} deletada com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar Bula de Aplicação com ID {id}: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante delete - {ex.Message}");
            }
        }
    }
}
