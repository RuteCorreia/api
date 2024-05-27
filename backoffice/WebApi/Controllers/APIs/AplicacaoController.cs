using Application.DTOs.Cadastros.Aplicacao.Interface;
using Application.DTOs.Cadastros.Aplicacao.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AplicacaoController : ControllerBase
    {
        private readonly IAplicacaoService _aplicacaoService;
        private readonly ILogService _logService;

        public AplicacaoController(IAplicacaoService aplicacaoService, ILogService logService)
        {
            _aplicacaoService = aplicacaoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AplicacaoViewModel>>> GetAll()
        {
            try
            {
                var aplicacoes = await _aplicacaoService.GetAllAsync();
                _logService.LogInformation("Todos os registros de Aplicação foram obtidos com sucesso.");
                return Ok(aplicacoes);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os registros de Aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os registros de Aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoViewModel>> GetById(int id)
        {
            try
            {
                var aplicacao = await _aplicacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(aplicacao))
                {
                    _logService.LogInformation($"Aplicação com ID {id} foi obtida com sucesso.");
                    return Ok(aplicacao);
                }

                _logService.LogWarning($"Aplicação com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter Aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter Aplicação com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoService.AddAsync(obj);
                    _logService.LogInformation("Nova Aplicação adicionada com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar nova Aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar nova Aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar nova Aplicação: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aplicacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _aplicacaoService.UpdateAsync(obj);
                        _logService.LogInformation($"Aplicação com ID {id} atualizada com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning($"Aplicação com ID {id} não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar Aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar Aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Aplicação com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoService.DeleteAsync(id);
                    _logService.LogInformation($"Aplicação com ID {id} deletada com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação não foi possível de ser executada ao deletar Aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar Aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Aplicação com ID {id}: {ex.Message}");
            }
        }
    }
}
