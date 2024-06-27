using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
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
    public class AplicacaoRelatorioController : ControllerBase
    {
        private readonly IAplicacaoRelatorioService _aplicacaoRelatorioService;
        private readonly ILogService _loggerService;

        public AplicacaoRelatorioController(IAplicacaoRelatorioService aplicacaoRelatorioService, ILogService loggerService)
        {
            _aplicacaoRelatorioService = aplicacaoRelatorioService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AplicacaoRelatorioViewModel>>> GetAll()
        {
            try
            {
                var relatorios = await _aplicacaoRelatorioService.GetAllAsync();
                _loggerService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoRelatorioViewModel>> GetById(int id)
        {
            try
            {
                var relatorio = await _aplicacaoRelatorioService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(relatorio))
                {
                    _loggerService.LogInformation($"O relatório de aplicação com ID {id} foi recuperado com sucesso.");
                    return Ok(relatorio);
                }

                _loggerService.LogWarning($"O relatório de aplicação com ID {id} não foi encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar o relatório de aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o relatório de aplicação: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoRelatorioViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoRelatorioService.AddAsync(obj);
                    _loggerService.LogInformation("Relatório de aplicação adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar um relatório de aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar relatório de aplicação: {ex.Message}");
            }
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRelatorioViewModel obj)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var existingObj = await _aplicacaoRelatorioService.GetByIdAsync(id);
        //            if (!ObjectNullValidation.IsObjectNull(existingObj))
        //            {
        //                obj.Id = existingObj.Id;
        //                await _aplicacaoRelatorioService.UpdateAsync(obj);
        //                _loggerService.LogInformation($"Relatório de aplicação com ID {id} atualizado com sucesso.");
        //                return Ok("Sucesso");
        //            }
        //            else
        //            {
        //                _loggerService.LogWarning($"O relatório de aplicação com ID {id} não foi encontrado.");
        //                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        //            }
        //        }

        //        _loggerService.LogWarning("Tentativa de atualizar um relatório de aplicação com um modelo inválido.");
        //        return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggerService.LogError(ex, $"Erro ao atualizar relatório de aplicação com ID {id}: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar relatório de aplicação: {ex.Message}");
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoRelatorioService.DeleteAsync(id);
                    _loggerService.LogInformation($"Relatório de aplicação com ID {id} deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar relatório de aplicação não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não pôde ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar relatório de aplicação: {ex.Message}");
            }
        }
    }
}
