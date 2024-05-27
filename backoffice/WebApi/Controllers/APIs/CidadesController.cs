using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Cidades.ViewModel;
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
    public class CidadesController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;
        private readonly ILogService _loggerService;

        public CidadesController(ICidadeService cidadeService, ILogService loggerService)
        {
            _cidadeService = cidadeService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<CidadeViewModel>>> GetAll()
        {
            try
            {
                var cidades = await _cidadeService.GetAllAsync();
                _loggerService.LogInformation("Todos os registros de cidades foram recuperados com sucesso.");
                return Ok(cidades);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os registros de cidades: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de cidades: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CidadeViewModel>> GetById(int id)
        {
            try
            {
                var cidade = await _cidadeService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(cidade))
                {
                    _loggerService.LogInformation($"Cidade com ID {id} foi recuperada com sucesso.");
                    return Ok(cidade);
                }

                _loggerService.LogWarning($"A cidade com ID {id} não foi encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar a cidade com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar a cidade: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CidadeViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _cidadeService.AddAsync(obj);
                    _loggerService.LogInformation("Cidade adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _loggerService.LogWarning("Tentativa de adicionar uma cidade com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar cidade: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar cidade: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CidadeViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _cidadeService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _cidadeService.UpdateAsync(obj);
                        _loggerService.LogInformation($"Cidade com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _loggerService.LogWarning($"A cidade com ID {id} não foi encontrada.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Tentativa de atualizar uma cidade com um modelo inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar cidade com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar cidade: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _cidadeService.DeleteAsync(id);
                    _loggerService.LogInformation($"Cidade com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _loggerService.LogWarning("Solicitação para deletar cidade não pôde ser executada, ID inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não pôde ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar cidade: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar cidade: {ex.Message}");
            }
        }
    }
}
