using Application.DTOs.Cadastros.AplicacaoCaracteristicas.Interface;
using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;
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
    public class AplicacaoCaracteristicasController : ControllerBase
    {
        private readonly IAplicacaoCaracteristicasService _aplicacaoCaracteristicasService;
        private readonly ILogService _logService;

        public AplicacaoCaracteristicasController(IAplicacaoCaracteristicasService aplicacaoCaracteristicasService, ILogService logService)
        {
            _aplicacaoCaracteristicasService = aplicacaoCaracteristicasService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AplicacaoCaracteristicasViewModel>>> GetAll()
        {
            try
            {
                var aplicacoesCaracteristicas = await _aplicacaoCaracteristicasService.GetAllAsync();
                _logService.LogInformation("Todos os registros de Aplicação de Características foram obtidos com sucesso.");
                return Ok(aplicacoesCaracteristicas);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os registros de Aplicação de Características: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os registros de Aplicação de Características: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoCaracteristicasViewModel>> GetById(int id)
        {
            try
            {
                var aplicacaoCaracteristicas = await _aplicacaoCaracteristicasService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(aplicacaoCaracteristicas))
                {
                    _logService.LogInformation($"Aplicação de Características com ID {id} foi obtida com sucesso.");
                    return Ok(aplicacaoCaracteristicas);
                }

                _logService.LogWarning($"Aplicação de Características com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter Aplicação de Características com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter Aplicação de Características com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoCaracteristicasViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoCaracteristicasService.AddAsync(obj);
                    _logService.LogInformation("Nova Aplicação de Características adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar nova Aplicação de Características.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar nova Aplicação de Características: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar nova Aplicação de Características: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCaracteristicasViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aplicacaoCaracteristicasService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _aplicacaoCaracteristicasService.UpdateAsync(obj);
                        _logService.LogInformation($"Aplicação de Características com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning($"Aplicação de Características com ID {id} não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar Aplicação de Características.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar Aplicação de Características com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Aplicação de Características com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoCaracteristicasService.DeleteAsync(id);
                    _logService.LogInformation($"Aplicação de Características com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação não foi possível de ser executada ao deletar Aplicação de Características.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar Aplicação de Características com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Aplicação de Características com ID {id}: {ex.Message}");
            }
        }
    }
}
