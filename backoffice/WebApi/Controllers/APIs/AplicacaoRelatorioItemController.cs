using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
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
    public class AplicacaoRelatorioItemController : ControllerBase
    {
        private readonly IAplicacaoRelatorioItemService _aplicacaoRelatorioItemService;
        private readonly ILogService _loggerService;

        public AplicacaoRelatorioItemController(IAplicacaoRelatorioItemService aplicacaoRelatorioItemService, ILogService loggerService)
        {
            _aplicacaoRelatorioItemService = aplicacaoRelatorioItemService;
            _loggerService = loggerService;
        }

        [HttpGet("getByIdAplicacaoRelatorio/{idAplicacaoRelatorio}")]
        public async Task<ActionResult<IEnumerable<AplicacaoRelatorioItemViewModel>>> GetAll(int idAplicacaoRelatorio)
        {
            try
            {
                var itens = await _aplicacaoRelatorioItemService.GetAllAsync(idAplicacaoRelatorio);
                _loggerService.LogInformation("Todos os itens do relatório de aplicação foram recuperados com sucesso.");
                return Ok(itens);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os itens do relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os itens do relatório de aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AplicacaoRelatorioItemViewModel>> GetById(int id)
        {
            try
            {
                var item = await _aplicacaoRelatorioItemService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(item))
                {
                    _loggerService.LogInformation($"O item do relatório de aplicação com ID {id} foi recuperado com sucesso.");
                    return Ok(item);
                }

                _loggerService.LogWarning($"O item do relatório de aplicação com ID {id} não foi encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar o item do relatório de aplicação com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar o item do relatório de aplicação: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoRelatorioItemViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoRelatorioItemService.AddAsync(obj);
                    _loggerService.LogInformation("Item do relatório de aplicação adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar um item do relatório de aplicação com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar item do relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar item do relatório de aplicação: {ex.Message}");
            }
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Update(int id, [FromBody] AplicacaoRelatorioItemViewModel obj)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var existingObj = await _aplicacaoRelatorioItemService.GetByIdAsync(id);
        //            if (!ObjectNullValidation.IsObjectNull(existingObj))
        //            {
        //                obj.Id = existingObj.Id;
        //                await _aplicacaoRelatorioItemService.UpdateAsync(obj);
        //                _loggerService.LogInformation($"Item do relatório de aplicação com ID {id} atualizado com sucesso.");
        //                return Ok("Sucesso");
        //            }
        //            else
        //            {
        //                _loggerService.LogWarning($"O item do relatório de aplicação com ID {id} não foi encontrado.");
        //                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        //            }
        //        }

        //        _loggerService.LogWarning("Tentativa de atualizar um item do relatório de aplicação com um modelo inválido.");
        //        return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggerService.LogError(ex, $"Erro ao atualizar item do relatório de aplicação com ID {id}: {ex.Message}");
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar item do relatório de aplicação: {ex.Message}");
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoRelatorioItemService.DeleteAsync(id);
                    _loggerService.LogInformation($"Item do relatório de aplicação com ID {id} deletado com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar item do relatório de aplicação não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não pôde ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar item do relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar item do relatório de aplicação: {ex.Message}");
            }
        }
    }
}
