using Application.DTOs.Cadastros.Precificacao.Interface;
using Application.DTOs.Cadastros.Precificacao.ViewModel;
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
    public class PrecificacaoController : ControllerBase
    {
        private readonly IPrecificacaoService _precificacaoService;
        private readonly ILogService _logService;

        public PrecificacaoController(IPrecificacaoService precificacaoService, ILogService logService)
        {
            _precificacaoService = precificacaoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PrecificacaoViewModel>>> GetAll()
        {
            try
            {
                var precificacoes = await _precificacaoService.GetAllAsync();
                _logService.LogInformation("Lista de precificações recuperada com sucesso.");
                return Ok(precificacoes);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todas as precificações: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todas as precificações: {ex.Message}");
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
                    _logService.LogInformation("Precificação recuperada com sucesso.");
                    return Ok(precificacao);
                }

                _logService.LogWarning("Precificação não encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Precificação não encontrada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar precificação pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar precificação pelo ID: {ex.Message}");
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
                    _logService.LogInformation("Nova precificação adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar nova precificação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar nova precificação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar nova precificação: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] PrecificacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _precificacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _precificacaoService.UpdateAsync(obj);
                        _logService.LogInformation("Precificação atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning("Precificação não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Precificação não encontrada");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar precificação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar precificação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar precificação: {ex.Message}");
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
                    _logService.LogInformation("Precificação deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação inválida para deletar precificação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar precificação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar precificação: {ex.Message}");
            }
        }
    }
}
