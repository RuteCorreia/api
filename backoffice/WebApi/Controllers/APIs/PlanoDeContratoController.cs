using Application.DTOs.Cadastros.PlanoDeContrato.Interface;
using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PlanoDeContratoController : ControllerBase
    {
        private readonly IPlanoDeContratoService _planoDeContratoService;
        private readonly ILogService _logService;

        public PlanoDeContratoController(IPlanoDeContratoService planoDeContratoService, ILogService logService)
        {
            _planoDeContratoService = planoDeContratoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PlanoDeContratoViewModel>>> GetAll()
        {
            try
            {
                var planos = await _planoDeContratoService.GetAllAsync();
                _logService.LogInformation("Planos de contrato recuperados com sucesso.");
                return Ok(planos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar planos de contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar planos de contrato: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlanoDeContratoViewModel>> GetById(int id)
        {
            try
            {
                var planoDeContrato = await _planoDeContratoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(planoDeContrato))
                {
                    _logService.LogInformation("Plano de contrato recuperado com sucesso.");
                    return Ok(planoDeContrato);
                }

                _logService.LogWarning("Plano de contrato não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Plano de contrato não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar plano de contrato pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar plano de contrato pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PlanoDeContratoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _planoDeContratoService.AddAsync(obj);
                    _logService.LogInformation("Plano de contrato adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar plano de contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar plano de contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar plano de contrato: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] PlanoDeContratoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _planoDeContratoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.IdPlano = objeto.IdPlano;
                        await _planoDeContratoService.UpdateAsync(obj);
                        _logService.LogInformation("Plano de contrato atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Plano de contrato não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Plano de contrato não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar plano de contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar plano de contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar plano de contrato: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _planoDeContratoService.DeleteAsync(id);
                    _logService.LogInformation("Plano de contrato deletado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para deletar plano de contrato.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação inválida");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar plano de contrato: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar plano de contrato: {ex.Message}");
            }
        }
    }
}
