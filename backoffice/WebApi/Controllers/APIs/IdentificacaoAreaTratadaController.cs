using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Log.Interface;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class IdentificacaoAreaTratadaController : ControllerBase
    {
        private readonly IIdentificacaoAreaTratadaService _identificacaoAreaTratadaService;
        private readonly ILogService _logService;

        public IdentificacaoAreaTratadaController(IIdentificacaoAreaTratadaService identificacaoAreaTratadaService, ILogService logService)
        {
            _identificacaoAreaTratadaService = identificacaoAreaTratadaService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<IdentificacaoAreaTratadaViewModel>>> GetAll()
        {
            try
            {
                var identificacoes = await _identificacaoAreaTratadaService.GetAllAsync();
                _logService.LogInformation("Lista de todas as identificações de área tratada obtida com sucesso.");
                return Ok(identificacoes);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todas as identificações de área tratada: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todas as identificações de área tratada: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaTratadaViewModel>> GetById(int id)
        {
            try
            {
                var identificacao = await _identificacaoAreaTratadaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(identificacao))
                {
                    _logService.LogInformation($"Detalhes da identificação de área tratada com ID {id} obtidos com sucesso.");
                    return Ok(identificacao);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes da identificação de área tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes da identificação de área tratada com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _identificacaoAreaTratadaService.AddAsync(obj);
                    _logService.LogInformation("Identificação de área tratada adicionada com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar identificação de área tratada: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar identificação de área tratada: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] IdentificacaoAreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingObj = await _identificacaoAreaTratadaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(existingObj))
                    {
                        obj.Id = existingObj.Id;
                        await _identificacaoAreaTratadaService.UpdateAsync(obj);
                        _logService.LogInformation($"Identificação de área tratada com ID {id} atualizada com sucesso.");
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
                _logService.LogError(ex, $"Erro ao atualizar identificação de área tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar identificação de área tratada com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _identificacaoAreaTratadaService.DeleteAsync(id);
                    _logService.LogInformation($"Identificação de área tratada com ID {id} excluída com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir identificação de área tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir identificação de área tratada com ID {id}: {ex.Message}");
            }
        }
    }
}
