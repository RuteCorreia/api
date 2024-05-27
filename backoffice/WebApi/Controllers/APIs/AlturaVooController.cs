using Application.DTOs.Cadastros.AlturaVoo.Interface;
using Application.DTOs.Cadastros.AlturaVoo.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class AlturaVooController : ControllerBase
    {
        private readonly IAlturaVooService _alturaVooService;
        private readonly ILogService _logService;

        public AlturaVooController(IAlturaVooService alturaVooService, ILogService logService)
        {
            _alturaVooService = alturaVooService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AlturaVooViewModel>>> GetAll()
        {
            try
            {
                var alturasVoo = await _alturaVooService.GetAllAsync();
                _logService.LogInformation("Todas as alturas de voo foram recuperadas com sucesso.");
                return Ok(alturasVoo);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao recuperar todas as alturas de voo.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlturaVooViewModel>> GetById(int id)
        {
            try
            {
                var alturaVoo = await _alturaVooService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(alturaVoo))
                {
                    _logService.LogInformation($"Altura de voo com ID {id} foi recuperada com sucesso.");
                    return Ok(alturaVoo);
                }

                _logService.LogWarning($"Altura de voo com ID {id} não encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar a altura de voo com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AlturaVooViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _alturaVooService.AddAsync(obj);
                    _logService.LogInformation("Nova altura de voo adicionada com sucesso.");
                    return Ok("Sucesso");
                }

                _logService.LogWarning("Modelo inválido ao tentar adicionar nova altura de voo.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao adicionar nova altura de voo.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AlturaVooViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _alturaVooService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _alturaVooService.UpdateAsync(obj);
                        _logService.LogInformation($"Altura de voo com ID {id} atualizada com sucesso.");
                        return Ok("Sucesso");
                    }
                    else
                    {
                        _logService.LogWarning($"Altura de voo com ID {id} não encontrada para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao tentar atualizar altura de voo.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar a altura de voo com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _alturaVooService.DeleteAsync(id);
                    _logService.LogInformation($"Altura de voo com ID {id} deletada com sucesso.");
                    return Ok("Deletado com sucesso");
                }

                _logService.LogWarning("Solicitação de exclusão com ID 0 é inválida.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir a altura de voo com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlturaVoo delete - {ex.Message}");
            }
        }
    }
}
