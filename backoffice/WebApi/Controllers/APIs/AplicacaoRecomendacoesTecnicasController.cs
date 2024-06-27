using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
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
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AplicacaoRecomendacoesTecnicasController : ControllerBase
    {
        private readonly IAplicacaoRecomendacoesTecnicasService _aplicacaoRecomendacoesTecnicasService;
        private readonly ILogService _loggerService;

        public AplicacaoRecomendacoesTecnicasController(IAplicacaoRecomendacoesTecnicasService aplicacaoRecomendacoesTecnicasService, ILogService loggerService)
        {
            _aplicacaoRecomendacoesTecnicasService = aplicacaoRecomendacoesTecnicasService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>>> GetAll()
        {
            try
            {
                var recomendacoes = await _aplicacaoRecomendacoesTecnicasService.GetAllAsync();
                _loggerService.LogInformation("Todos os registros de recomendações técnicas de aplicação foram recuperados com sucesso.");
                return Ok(recomendacoes);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todas as recomendações técnicas de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todas as recomendações técnicas de aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoRecomendacoesTecnicasViewModel>> GetById(int id)
        {
            try
            {
                var recomendacao = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(recomendacao))
                {
                    _loggerService.LogInformation($"A recomendação técnica de aplicação com ID {id} foi recuperada com sucesso.");
                    return Ok(recomendacao);
                }

                _loggerService.LogWarning($"A recomendação técnica de aplicação com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar a recomendação técnica de aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar a recomendação técnica de aplicação: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoRecomendacoesTecnicasViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoRecomendacoesTecnicasService.AddAsync(obj);
                    _loggerService.LogInformation("Recomendação técnica de aplicação adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar uma recomendação técnica de aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar recomendação técnica de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar recomendação técnica de aplicação: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRecomendacoesTecnicasViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingObj = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(existingObj))
                    {
                        obj.Id = existingObj.Id;
                        await _aplicacaoRecomendacoesTecnicasService.UpdateAsync(obj);
                        _loggerService.LogInformation($"Recomendação técnica de aplicação com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _loggerService.LogWarning($"A recomendação técnica de aplicação com ID {id} não foi encontrada.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Tentativa de atualizar uma recomendação técnica de aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar recomendação técnica de aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar recomendação técnica de aplicação: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoRecomendacoesTecnicasService.DeleteAsync(id);
                    _loggerService.LogInformation($"Recomendação técnica de aplicação com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar recomendação técnica de aplicação não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não pôde ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar recomendação técnica de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar recomendação técnica de aplicação: {ex.Message}");
            }
        }
    }
}
