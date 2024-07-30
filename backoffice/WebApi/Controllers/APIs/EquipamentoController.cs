using Application.DTOs.Cadastros.Equipamento.Interface;
using Application.DTOs.Cadastros.Equipamento.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;
        private readonly ILogService _logService;

        public EquipamentoController(IEquipamentoService equipamentoService, ILogService logService)
        {
            _equipamentoService = equipamentoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<EquipamentoViewModel>>> GetAll()
        {
            try
            {
                var equipamentos = await _equipamentoService.GetAllAsync();
                _logService.LogInformation("Lista de todos os equipamentos obtida com sucesso.");
                return Ok(equipamentos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os equipamentos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os equipamentos: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EquipamentoViewModel>> GetById(int id)
        {
            try
            {
                var equipamento = await _equipamentoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(equipamento))
                {
                    _logService.LogInformation($"Detalhes do equipamento com ID {id} obtidos com sucesso.");
                    return Ok(equipamento);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes do equipamento com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes do equipamento com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EquipamentoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _equipamentoService.AddAsync(obj);
                    _logService.LogInformation($"Equipamento adicionado com sucesso: {obj}");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar equipamento: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar equipamento: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] EquipamentoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _equipamentoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;
                        await _equipamentoService.UpdateAsync(obj);
                        _logService.LogInformation($"Equipamento com ID {id} atualizado com sucesso.");
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
                _logService.LogError(ex, $"Erro ao atualizar equipamento com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar equipamento com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _equipamentoService.DeleteAsync(id);
                    _logService.LogInformation($"Equipamento com ID {id} excluído com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir equipamento com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir equipamento com ID {id}: {ex.Message}");
            }
        }
    }
}
