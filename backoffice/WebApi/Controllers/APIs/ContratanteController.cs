using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Application.DTOs.Log.Interface;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ContratanteController : ControllerBase
    {
        private readonly IContratanteService _contratanteService;
        private readonly ILogService _loggerService;

        public ContratanteController(IContratanteService contratanteService, ILogService loggerService)
        {
            _contratanteService = contratanteService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ContratanteViewModel>>> GetAll()
        {
            try
            {
                var contratante = await _contratanteService.GetAllAsync();
                _loggerService.LogInformation("Todos os contratantes foram recuperados com sucesso.");
                return Ok(contratante);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os contratantes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os contratantes: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContratanteViewModel>> GetById(int id)
        {
            try
            {
                var contratante = await _contratanteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(contratante))
                {
                    _loggerService.LogInformation($"Contratante com ID {id} foi recuperado com sucesso.");
                    return Ok(contratante);
                }

                _loggerService.LogWarning($"Contratante com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar contratante com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar contratante com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ContratanteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contratanteService.AddAsync(obj);
                    _loggerService.LogInformation("Novo contratante adicionado com sucesso.");
                    return Ok();
                }

                _loggerService.LogWarning("Modelo inválido ao adicionar novo contratante.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar novo contratante: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo contratante: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ContratanteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _contratanteService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _contratanteService.UpdateAsync(obj);
                        _loggerService.LogInformation($"Contratante com ID {id} atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _loggerService.LogWarning($"Contratante com ID {id} não encontrado.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Modelo inválido ao atualizar contratante.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar contratante com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar contratante com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _contratanteService.DeleteAsync(id);
                    _loggerService.LogInformation($"Contratante com ID {id} deletado com sucesso.");
                    return Ok();
                }

                _loggerService.LogWarning("ID inválido ao tentar deletar contratante.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar contratante com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar contratante com ID {id}: {ex.Message}");
            }
        }
    }
}
