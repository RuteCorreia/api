using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.Aeronave.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Cadastros.Empresa.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronave.Interface;
using Application.DTOs.Cadastros.Componentes.Interface;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.Interface;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AeronaveController : ControllerBase
    {
        private readonly IAeronaveService _aeronaveService;
        private readonly IManutencaoAeronaveService _manutencaoAeronaveService;
        private readonly IComponentesService _componentesService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _logService;

        public AeronaveController(IAeronaveService aeronaveService, LoggedUserInfoService loggedUserInfoService, ILogService logService, IManutencaoAeronaveService manutencaoAeronaveService, IComponentesService componentesService)
        {
            _aeronaveService = aeronaveService;
            _loggedUserInfoService = loggedUserInfoService;
            _logService = logService;
            _manutencaoAeronaveService = manutencaoAeronaveService;
            _componentesService = componentesService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AeronaveViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var aeronaves = await _aeronaveService.GetAllAsync(loggedUser.Item3);

                foreach (var aeronave in aeronaves)
                {
                    var manutencao = await _manutencaoAeronaveService.GetByIdAeronaveAsync(aeronave.Id);
                    var componentes = await _componentesService.GetByIdAeronaveAsync(aeronave.Id);
                    aeronave.ItensRevisao = manutencao.SelectMany(s => s.ItensRevisao);
                    aeronave.Componentes = componentes;
                }

                _logService.LogInformation("Todas as aeronaves foram recuperadas com sucesso.");
                return Ok(aeronaves);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao recuperar todas as aeronaves.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AeronaveViewModel>> GetById(int id)
        {
            try
            {
                var aeronave = await _aeronaveService.GetByIdAsync(id);
                var manutencao = await _manutencaoAeronaveService.GetByIdAeronaveAsync(id);
                var componentes = await _componentesService.GetByIdAeronaveAsync(id);
                aeronave.ItensRevisao = manutencao.SelectMany(s => s.ItensRevisao);
                aeronave.Componentes = componentes;

                if (!ObjectNullValidation.IsObjectNull(aeronave))
                {
                    _logService.LogInformation($"Aeronave com ID {id} foi recuperada com sucesso.");
                    return Ok(aeronave);
                }

                _logService.LogWarning($"Aeronave com ID {id} não encontrada.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar a aeronave com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AeronaveViewModel obj)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var aeronaves = await _aeronaveService.GetAllAsync(loggedUser.Item3);
                if (!aeronaves.Any(x => string.Equals(x.Prefixo?.ToLower(), obj.Prefixo?.ToLower())))
                {
                    if (ModelState.IsValid)
                    {
                        await _aeronaveService.AddAsync(obj, loggedUser.Item3);
                        _logService.LogInformation("Nova aeronave adicionada com sucesso.");
                        return Ok();
                    }
                }

                _logService.LogWarning("Prefixo já existe, não é possível adicionar duplicado.");
                return StatusCode(StatusCodes.Status400BadRequest, "Prefixo não pode ser duplicado (já existe outra aeronave com esse prefixo)");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao adicionar nova aeronave.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AeronaveViewModel obj)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var aeronaves = await _aeronaveService.GetAllAsync(loggedUser.Item3);
                if (!aeronaves.Any(x => string.Equals(x.Prefixo?.ToLower(), obj.Prefixo?.ToLower()) && x.Id != obj.Id))
                {
                    if (ModelState.IsValid)
                    {
                        var objeto = await _aeronaveService.GetByIdAsync(id);
                        if (!ObjectNullValidation.IsObjectNull(objeto))
                        {
                            obj.Id = objeto.Id;

                            await _aeronaveService.UpdateAsync(obj);
                            _logService.LogInformation($"Aeronave com ID {id} atualizada com sucesso.");
                            return Ok();
                        }
                        else
                        {
                            _logService.LogWarning($"Aeronave com ID {id} não encontrada para atualização.");
                            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                        }
                    }
                }

                _logService.LogWarning("Prefixo já existe, não é possível atualizar com duplicado.");
                return StatusCode(StatusCodes.Status400BadRequest, "Prefixo não pode ser duplicado (já existe outra aeronave com esse prefixo)");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar aeronave com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave update - {ex.Message}");
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<IAsyncEnumerable<AeronaveViewModel>>> GetByName(string name)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var aeronaves = await _aeronaveService.GetByNameAsync(name, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(aeronaves))
                {
                    _logService.LogInformation($"Aeronaves com Nome {name} foram recuperadas com sucesso.");
                    return Ok(aeronaves);
                }

                _logService.LogWarning($"Aeronaves com Nome {name} não encontradas.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar as aeronaves com Nome {name}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave getByName - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _aeronaveService.DeleteAsync(id);
                    _logService.LogInformation($"Aeronave com ID {id} excluída com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação de exclusão com ID 0 é inválida.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir aeronave com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Aeronave delete - {ex.Message}");
            }
        }
    }
}
