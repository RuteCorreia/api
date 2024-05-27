using Application.DTOs.Cadastros.Frota.Interface;
using Application.DTOs.Cadastros.Frota.ViewModel;
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
    public class FrotaController : ControllerBase
    {
        private readonly IFrotaService _frotaService;
        private readonly ILogService _logService;

        public FrotaController(IFrotaService frotaService, ILogService logService)
        {
            _frotaService = frotaService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<FrotaViewModel>>> GetAll()
        {
            try
            {
                var frota = await _frotaService.GetAllAsync();
                _logService.LogInformation("Lista de todos os itens da frota obtida com sucesso.");
                return Ok(frota);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os itens da frota: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os itens da frota: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FrotaViewModel>> GetById(int id)
        {
            try
            {
                var itemFrota = await _frotaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(itemFrota))
                {
                    _logService.LogInformation($"Detalhes do item da frota com ID {id} obtidos com sucesso.");
                    return Ok(itemFrota);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes do item da frota com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes do item da frota com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FrotaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _frotaService.AddAsync(obj);
                    _logService.LogInformation("Item da frota adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar item da frota: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar item da frota: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] FrotaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingItem = await _frotaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(existingItem))
                    {
                        obj.Id = existingItem.Id;
                        await _frotaService.UpdateAsync(obj);
                        _logService.LogInformation($"Item da frota com ID {id} atualizado com sucesso.");
                        return Ok("Sucesso");
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
                _logService.LogError(ex, $"Erro ao atualizar item da frota com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar item da frota com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _frotaService.DeleteAsync(id);
                    _logService.LogInformation($"Item da frota com ID {id} excluído com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir item da frota com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir item da frota com ID {id}: {ex.Message}");
            }
        }
    }
}
