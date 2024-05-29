using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Interface;
using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;
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
    public class AplicacaoCroquiImportacaoController : ControllerBase
    {
        private readonly IAplicacaoCroquiImportacaoService _aplicacaoCroquiImportacaoService;
        private readonly ILogService _loggerService;

        public AplicacaoCroquiImportacaoController(IAplicacaoCroquiImportacaoService aplicacaoCroquiImportacaoService, ILogService loggerService)
        {
            _aplicacaoCroquiImportacaoService = aplicacaoCroquiImportacaoService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AplicacaoCroquiImportacaoViewModel>>> GetAll()
        {
            try
            {
                var croquis = await _aplicacaoCroquiImportacaoService.GetAllAsync();
                return Ok(croquis);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao obter todos os AplicacaoCroquiImportacao: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os AplicacaoCroquiImportacao: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoCroquiImportacaoViewModel>> GetById(int id)
        {
            try
            {
                var croqui = await _aplicacaoCroquiImportacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(croqui))
                {
                    return Ok(croqui);
                }

                _loggerService.LogWarning($"AplicacaoCroquiImportacao com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao obter AplicacaoCroquiImportacao com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter AplicacaoCroquiImportacao: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoCroquiImportacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoCroquiImportacaoService.AddAsync(obj);
                    _loggerService.LogInformation("AplicacaoCroquiImportacao adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar AplicacaoCroquiImportacao com modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar AplicacaoCroquiImportacao: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar AplicacaoCroquiImportacao: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoCroquiImportacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aplicacaoCroquiImportacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;
                        await _aplicacaoCroquiImportacaoService.UpdateAsync(obj);
                        _loggerService.LogInformation($"AplicacaoCroquiImportacao com ID {id} atualizado com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _loggerService.LogWarning($"AplicacaoCroquiImportacao com ID {id} não encontrado.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Tentativa de atualizar AplicacaoCroquiImportacao com modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar AplicacaoCroquiImportacao com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar AplicacaoCroquiImportacao: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoCroquiImportacaoService.DeleteAsync(id);
                    _loggerService.LogInformation($"AplicacaoCroquiImportacao com ID {id} deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar AplicacaoCroquiImportacao não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar AplicacaoCroquiImportacao com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar AplicacaoCroquiImportacao: {ex.Message}");
            }
        }
    }
}
