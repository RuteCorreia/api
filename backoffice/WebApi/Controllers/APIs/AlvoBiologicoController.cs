using Application.DTOs.Cadastros.Alvo_Biologico.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class AlvoBiologicoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IAlvoBiologicoService _alvoBiologicoService;
        private readonly ILogService _logService;

        public AlvoBiologicoController(
            LoggedUserInfoService loggedUserInfoService,
            IAlvoBiologicoService alvoBiologicoService, 
            ILogService logService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _alvoBiologicoService = alvoBiologicoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AlvoBiologicoViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var alvosBiologicos = await _alvoBiologicoService.GetAllAsync(loggedUser.Item3);
                _logService.LogInformation("Todas os alvos biológicos foram recuperados com sucesso.");
                return Ok(alvosBiologicos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao recuperar todos os alvos biológicos.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpGet("DropDownListAlvosBiologicos")]
        public async Task<ActionResult<IAsyncEnumerable<AlvoBiologicoViewModel>>> GetAlvosBiologicos([FromQuery] string nomeCultura, [FromQuery] string nomeProduto)
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var alvosBiologicos = await _alvoBiologicoService.GetAlvosBiologicosAsync(nomeCultura, nomeProduto, loggedUser.Item3);
            return Ok(alvosBiologicos);
        }

        [HttpGet("getByIdBula/{id:int}")]
        public async Task<ActionResult<IAsyncEnumerable<FormulacaoViewModel>>> GetByIdBula(int id)
        {
            try
            {
                var result = await _alvoBiologicoService.GetFormulacaoAsync(id);
                _logService.LogInformation("Todas os alvos biológicos foram recuperados com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao recuperar todos os alvos biológicos.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }

        [HttpPut("updateRecomendacao")]
        public async Task<ActionResult> UpdateRecomendacao([FromBody] FormulacaoViewModel obj)
        {
            try
            {
                await _alvoBiologicoService.UpdateFormulacaoAsync(obj);
                _logService.LogInformation("Todas os alvos biológicos foram atualizados com sucesso.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao recuperar todos os alvos biológicos.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico updateRecomendacao - {ex.Message}");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlvoBiologicoViewModel>> GetById(int id)
        {
            try
            {
                var alvoBiologico = await _alvoBiologicoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(alvoBiologico))
                {
                    _logService.LogInformation($"Alvo biológico com ID {id} foi recuperado com sucesso.");
                    return Ok(alvoBiologico);
                }

                _logService.LogWarning($"Alvo biológico com ID {id} não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar o alvo biológico com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getById - {ex.Message}");
            }
        }

        [HttpGet("GetByIdCultura/{id:int}")]
        public async Task<ActionResult<IAsyncEnumerable<AlvoBiologicoViewModel>>> GetByIdCultura(int id)
        {
            try
            {
                var alvosBiologicos = await _alvoBiologicoService.GetByIdCulturaAsync(id);
                if (!ObjectNullValidation.IsObjectNull(alvosBiologicos))
                {
                    _logService.LogInformation($"Alvos biológicos com ID Cultura {id} foram recuperados com sucesso.");
                    return Ok(alvosBiologicos);
                }

                _logService.LogWarning($"Alvos biológicos com ID Cultura {id} não encontrados.");
                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar os alvos biológicos com ID Cultura {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getByIdCultura - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AlvoBiologicoViewModel obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var verificaSeAlvoExistePeloNome = await _alvoBiologicoService.GetByName(obj.Nome, loggedUser.Item3);
                    if (verificaSeAlvoExistePeloNome != null)
                    {
                        _logService.LogWarning("Tentativa de adição de um alvo biologico com nome duplicado"); // Registre um aviso de log
                        return StatusCode(StatusCodes.Status409Conflict, "Já existe um alvo com esse nome!");
                    }
                    await _alvoBiologicoService.AddAsync(obj, loggedUser.Item3);
                    _logService.LogInformation("Novo alvo biológico adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao tentar adicionar novo alvo biológico.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Erro ao adicionar novo alvo biológico.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] AlvoBiologicoViewModel obj)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var verificaSeAlvoBiologicoExistePeloNome = await _alvoBiologicoService.GetByName(obj.Nome, loggedUser.Item3);
                if (verificaSeAlvoBiologicoExistePeloNome != null && verificaSeAlvoBiologicoExistePeloNome.Id != obj.Id)
                {
                    _logService.LogWarning("Tentativa de atualização de alvo biológico com nome já existente.");
                    return StatusCode(StatusCodes.Status400BadRequest, "Já existe um alvo biológico com esse nome!");
                }
                if (ModelState.IsValid)
                {
                    var objeto = await _alvoBiologicoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _alvoBiologicoService.UpdateAsync(obj);
                        _logService.LogInformation($"Alvo biológico com ID {id} atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning($"Alvo biológico com ID {id} não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao tentar atualizar alvo biológico.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar o alvo biológico com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _alvoBiologicoService.DeleteAsync(id);
                    _logService.LogInformation($"Alvo biológico com ID {id} deletado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação de exclusão com ID 0 é inválida.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir o alvo biológico com ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico delete - {ex.Message}");
            }
        }
    }
}
