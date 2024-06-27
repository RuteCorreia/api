using Application.DTOs.Cadastros.AplicacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
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
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AplicacaoAreaTratadaController : ControllerBase
    {
        private readonly IAplicacaoAreaTratadaService _aplicacaoAreaTratadaService;
        private readonly ILogService _logService;

        public AplicacaoAreaTratadaController(IAplicacaoAreaTratadaService aplicacaoAreaTratadaService, ILogService logService)
        {
            _aplicacaoAreaTratadaService = aplicacaoAreaTratadaService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AplicacaoAreaTratadaViewModel>>> GetAll()
        {
            try
            {
                var aplicacoesAreaTratada = await _aplicacaoAreaTratadaService.GetAllAsync();
                _logService.LogInformation("Todos os registros de Aplicação de Área Tratada foram obtidos com sucesso.");
                return Ok(aplicacoesAreaTratada);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os registros de Aplicação de Área Tratada: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os registros de Aplicação de Área Tratada: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AplicacaoAreaTratadaViewModel>> GetById(int id)
        {
            try
            {
                var aplicacaoAreaTratada = await _aplicacaoAreaTratadaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(aplicacaoAreaTratada))
                {
                    _logService.LogInformation($"Aplicação de Área Tratada com ID {id} foi obtida com sucesso.");
                    return Ok(aplicacaoAreaTratada);
                }

                _logService.LogWarning($"Aplicação de Área Tratada com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter Aplicação de Área Tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter Aplicação de Área Tratada com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AplicacaoAreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aplicacaoAreaTratadaService.AddAsync(obj);
                    _logService.LogInformation("Nova Aplicação de Área Tratada adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao adicionar nova Aplicação de Área Tratada.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar nova Aplicação de Área Tratada: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar nova Aplicação de Área Tratada: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AplicacaoAreaTratadaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _aplicacaoAreaTratadaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _aplicacaoAreaTratadaService.UpdateAsync(obj);
                        _logService.LogInformation($"Aplicação de Área Tratada com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning($"Aplicação de Área Tratada com ID {id} não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar Aplicação de Área Tratada.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar Aplicação de Área Tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Aplicação de Área Tratada com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aplicacaoAreaTratadaService.DeleteAsync(id);
                    _logService.LogInformation($"Aplicação de Área Tratada com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação inválida para deletar Aplicação de Área Tratada com ID 0.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar Aplicação de Área Tratada com ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar Aplicação de Área Tratada com ID {id}: {ex.Message}");
            }
        }
    }
}
