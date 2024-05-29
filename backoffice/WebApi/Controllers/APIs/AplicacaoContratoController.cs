using Application.DTOs.Cadastros.AplicacaoContrato.Interface;
using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;
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
    public class AplicacaoContratoController : ControllerBase
    {
        private readonly IAplicacaoContratoService _aplicacaoContratoService;
        private readonly ILogService _logService;

        public AplicacaoContratoController(IAplicacaoContratoService aplicacaoContratoService, ILogService logService)
        {
            _aplicacaoContratoService = aplicacaoContratoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AplicacaoContratoViewModel>>> GetAll()
        {
            try
            {
                var aplicacoesContrato = await _aplicacaoContratoService.GetAllAsync();
                _logService.LogInformation("Todos os registros de Aplicação de Contrato foram obtidos com sucesso.");
                return Ok(aplicacoesContrato);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os registros de Aplicação de Contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os registros de Aplicação de Contrato: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoContratoViewModel>> GetById(int id)
        {
            try
            {
                var aplicacaoContrato = await _aplicacaoContratoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(aplicacaoContrato))
                {
                    _logService.LogInformation($"Aplicação de Contrato com ID {id} foi obtida com sucesso.");
                    return Ok(aplicacaoContrato);
                }

                _logService.LogWarning($"Aplicação de Contrato com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter Aplicação de Contrato com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter Aplicação de Contrato com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoContratoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoContratoService.AddAsync(obj);
                    _logService.LogInformation("Nova Aplicação de Contrato adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar nova Aplicação de Contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar nova Aplicação de Contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar nova Aplicação de Contrato: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoContratoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aplicacaoContratoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _aplicacaoContratoService.UpdateAsync(obj);
                        _logService.LogInformation($"Aplicação de Contrato com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning($"Aplicação de Contrato com ID {id} não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar Aplicação de Contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar Aplicação de Contrato com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Aplicação de Contrato com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoContratoService.DeleteAsync(id);
                    _logService.LogInformation($"Aplicação de Contrato com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação não foi possível de ser executada ao deletar Aplicação de Contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar Aplicação de Contrato com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Aplicação de Contrato com ID {id}: {ex.Message}");
            }
        }
    }
}
