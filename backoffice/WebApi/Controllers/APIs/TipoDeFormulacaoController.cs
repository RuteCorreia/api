using Application.Application.Servicos.Log;
using Application.DTOs.Cadastros.TipoDeFormulacao.Interfaces;
using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class TipoDeFormulacaoController : ControllerBase
    {
        private readonly ITipoDeFormulacaoService _tipoDeFormulacaoService;
        private readonly ILogService _logService;

        public TipoDeFormulacaoController(ITipoDeFormulacaoService tipoDeFormulacaoService, ILogService logService)
        {
            _tipoDeFormulacaoService = tipoDeFormulacaoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<TipoDeFormulacaoViewModel>>> GetAll()
        {
            try
            {
                var result = await _tipoDeFormulacaoService.GetAllAsync();
                _logService.LogInformation("Todos os tipos de produto foram recuperados com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os tipos de produto: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoDeFormulacaoViewModel>> GetById(int id)
        {
            try
            {
                var result = await _tipoDeFormulacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(result))
                {
                    _logService.LogInformation("Tipo de produto recuperado com sucesso.");
                    return Ok(result);
                }

                _logService.LogWarning("Tipo de produto não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Tipo de produto não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar tipo de produto pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar tipo de produto pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TipoDeFormulacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _tipoDeFormulacaoService.AddAsync(obj);
                    _logService.LogInformation("Novo tipo de produto adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar novo tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo tipo de produto: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] TipoDeFormulacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _tipoDeFormulacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _tipoDeFormulacaoService.UpdateAsync(obj);
                        _logService.LogInformation("Tipo de produto atualizado com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning("Tipo de produto não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Tipo de produto não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar tipo de produto: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _tipoDeFormulacaoService.DeleteAsync(id);
                    _logService.LogInformation("Tipo de produto deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação inválida para deletar tipo de produto.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar tipo de produto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar tipo de produto: {ex.Message}");
            }
        }
    }
}
