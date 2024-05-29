using Application.DTOs.Cadastros.AplicacaoLog.Interface;
using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AplicacaoLogController : ControllerBase
    {
        private readonly IAplicacaoLogService _aplicacaoLogService;
        private readonly ILogService _loggerService;

        public AplicacaoLogController(IAplicacaoLogService aplicacaoLogService, ILogService loggerService)
        {
            _aplicacaoLogService = aplicacaoLogService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AplicacaoLogViewModel>>> GetAll()
        {
            try
            {
                var logs = await _aplicacaoLogService.GetAllAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao obter todos os logs da aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os logs da aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoLogViewModel>> GetById(int id)
        {
            try
            {
                var log = await _aplicacaoLogService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(log))
                {
                    return Ok(log);
                }

                _loggerService.LogWarning($"Log da aplicação com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao obter log da aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter log da aplicação: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoLogViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoLogService.AddAsync(obj);
                    _loggerService.LogInformation("Log da aplicação adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar log da aplicação com modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar log da aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar log da aplicação: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoLogViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var log = await _aplicacaoLogService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(log))
                    {
                        obj.Id = log.Id;
                        await _aplicacaoLogService.UpdateAsync(obj);
                        _loggerService.LogInformation($"Log da aplicação com ID {id} atualizado com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _loggerService.LogWarning($"Log da aplicação com ID {id} não encontrado.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Tentativa de atualizar log da aplicação com modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar log da aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar log da aplicação: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoLogService.DeleteAsync(id);
                    _loggerService.LogInformation($"Log da aplicação com ID {id} deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar log da aplicação não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar log da aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar log da aplicação: {ex.Message}");
            }
        }
    }
}
