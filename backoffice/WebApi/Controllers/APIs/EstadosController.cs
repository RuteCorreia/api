using Application.DTOs.Cadastros.Estados.Interface;
using Application.DTOs.Cadastros.Estados.ViewModel;
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
    public class EstadosController : ControllerBase
    {
        private readonly IEstadosService _estadosService;
        private readonly ILogService _logService;

        public EstadosController(IEstadosService estadosService, ILogService logService)
        {
            _estadosService = estadosService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<EstadosViewModel>>> GetAll()
        {
            try
            {
                var estados = await _estadosService.GetAllAsync();
                _logService.LogInformation("Lista de todos os estados obtida com sucesso.");
                return Ok(estados);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todos os estados: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todos os estados: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstadosViewModel>> GetById(int id)
        {
            try
            {
                var estados = await _estadosService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(estados))
                {
                    _logService.LogInformation($"Detalhes do estado com ID {id} obtidos com sucesso.");
                    return Ok(estados);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes do estado com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes do estado com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EstadosViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _estadosService.AddAsync(obj);
                    _logService.LogInformation("Estado adicionado com sucesso.");
                    return Ok("Sucesso");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar estado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar estado: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] EstadosViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _estadosService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _estadosService.UpdateAsync(obj);
                        _logService.LogInformation($"Estado com ID {id} atualizado com sucesso.");
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
                _logService.LogError(ex, $"Erro ao atualizar estado com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar estado com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _estadosService.DeleteAsync(id);
                    _logService.LogInformation($"Estado com ID {id} excluído com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir estado com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir estado com ID {id}: {ex.Message}");
            }
        }
    }
}
