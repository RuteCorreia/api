using Application.DTOs.Cadastros.Componentes.Interface;
using Application.DTOs.Cadastros.Componentes.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ComponenteController : ControllerBase
    {
        private readonly IComponentesService _componenteService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _loggerService;

        public ComponenteController(
            IComponentesService componenteService,
            LoggedUserInfoService loggedUserInfoService,
            ILogService loggerService
        )
        {
            _componenteService = componenteService;
            _loggedUserInfoService = loggedUserInfoService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ComponentesViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var componentes = await _componenteService.GetAllAsync(loggedUser.Item3);
                _loggerService.LogInformation("Todos os componentes foram recuperados com sucesso.");
                return Ok(componentes);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os componentes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os componentes: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComponentesViewModel>> GetById(int id)
        {
            try
            {
                var componente = await _componenteService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(componente))
                {
                    _loggerService.LogInformation($"Componente com ID {id} foi recuperado com sucesso.");
                    return Ok(componente);
                }

                _loggerService.LogWarning($"Componente com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar componente com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar componente com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ComponentesViewModel obj)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                if (ModelState.IsValid)
                {
                    await _componenteService.AddAsync(obj, loggedUser.Item3);
                    _loggerService.LogInformation("Novo componente adicionado com sucesso.");
                    return Ok();
                }

                _loggerService.LogWarning("Modelo inválido ao adicionar novo componente.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar novo componente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo componente: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ComponentesViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _componenteService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _componenteService.UpdateAsync(obj);
                        _loggerService.LogInformation($"Componente com ID {id} atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _loggerService.LogWarning($"Componente com ID {id} não encontrado.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _loggerService.LogWarning("Modelo inválido ao atualizar componente.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao atualizar componente com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar componente com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _componenteService.DeleteAsync(id);
                    _loggerService.LogInformation($"Componente com ID {id} deletado com sucesso.");
                    return Ok();
                }

                _loggerService.LogWarning("ID inválido ao tentar deletar componente.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar componente com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar componente com ID {id}: {ex.Message}");
            }
        }
    }
}
