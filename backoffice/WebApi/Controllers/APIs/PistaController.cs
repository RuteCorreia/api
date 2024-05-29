using Application.DTOs.Cadastros.Pistas.Interface;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PistaController : ControllerBase
    {
        private readonly IPistaService _pistaService;
        private readonly ILogService _logService;

        public PistaController(IPistaService pistaService, ILogService logService)
        {
            _pistaService = pistaService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PistaViewModel>>> GetAll()
        {
            try
            {
                var pistas = await _pistaService.GetAllAsync();
                _logService.LogInformation("Pistas recuperadas com sucesso.");
                return Ok(pistas);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar pistas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pistas: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PistaViewModel>> GetById(int id)
        {
            try
            {
                var pista = await _pistaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(pista))
                {
                    _logService.LogInformation("Pista recuperada com sucesso.");
                    return Ok(pista);
                }

                _logService.LogWarning("Pista não encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Pista não encontrada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar pista pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pista pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PistaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _pistaService.AddAsync(obj);
                    _logService.LogInformation("Pista adicionada com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar pista.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar pista: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar pista: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] PistaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _pistaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;
                        await _pistaService.UpdateAsync(obj);
                        _logService.LogInformation("Pista atualizada com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Pista não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Pista não encontrada");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar pista.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar pista: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar pista: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _pistaService.DeleteAsync(id);
                    _logService.LogInformation("Pista deletada com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para deletar pista.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação inválida");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar pista: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar pista: {ex.Message}");
            }
        }
    }
}
