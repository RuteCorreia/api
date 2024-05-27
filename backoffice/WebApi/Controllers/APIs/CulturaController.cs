using Application.DTOs.Cadastros.Cultura.Interface;
using Application.DTOs.Cadastros.Cultura.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Application.Servicos.Log; // Importe o serviço de log
using Application.DTOs.Log.Interface; // Importe a interface do serviço de log
using Microsoft.AspNetCore.Http;
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
    public class CulturaController : ControllerBase
    {
        private readonly ICulturaService _culturaService;
        private readonly ILogService _logService; // Injete o serviço de log

        public CulturaController(ICulturaService culturaService, ILogService logService) // Adicione o serviço de log como parâmetro do construtor
        {
            _culturaService = culturaService;
            _logService = logService; // Atribua o serviço de log
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<CulturaViewModel>>> GetAll()
        {
            try
            {
                var culturas = await _culturaService.GetAllAsync();
                return Ok(culturas);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Cultura getAll - {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura getAll - {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CulturaViewModel>> GetById(int id)
        {
            try
            {
                var cultura = await _culturaService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(cultura))
                {
                    return Ok(cultura);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Cultura getById - {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura getById - {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CulturaViewModel obj)
        {
            try
            {
                var verificaSeCulturaExistePeloNome = await _culturaService.GetByName(obj.Nome);
                if (verificaSeCulturaExistePeloNome != null)
                {
                    _logService.LogWarning("Tentativa de adição de uma cultura com nome duplicado"); // Registre um aviso de log
                    return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma cultura com esse nome!");
                }

                if (ModelState.IsValid)
                {
                    await _culturaService.AddAsync(obj);
                    _logService.LogInformation("Cultura adicionada com sucesso"); // Registre uma informação de log
                    return Ok();
                }

                _logService.LogWarning("Tentativa de adição de uma cultura com modelo inválido"); // Registre um aviso de log
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar Cultura: {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura add - {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CulturaViewModel obj)
        {
            try
            {
                var verificaSeCulturaExistePeloNome = await _culturaService.GetByName(obj.Nome);
                if (verificaSeCulturaExistePeloNome != null && verificaSeCulturaExistePeloNome.IdCultura != obj.IdCultura)
                {
                    _logService.LogWarning("Tentativa de atualização de uma cultura com nome duplicado"); // Registre um aviso de log
                    return StatusCode(StatusCodes.Status400BadRequest, "Já existe uma cultura com esse nome!");
                }
                if (ModelState.IsValid)
                {
                    var objeto = await _culturaService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.IdCultura = objeto.IdCultura;

                        await _culturaService.UpdateAsync(obj);
                        _logService.LogInformation("Cultura atualizada com sucesso"); // Registre uma informação de log
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Tentativa de atualização de uma cultura que não foi encontrada"); // Registre um aviso de log
                        return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
                    }
                }

                _logService.LogWarning("Tentativa de atualização de uma cultura com modelo inválido"); // Registre um aviso de log
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar Cultura: {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura update - {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _culturaService.DeleteAsync(id);
                    _logService.LogInformation("Cultura deletada com sucesso"); // Registre uma informação de log
                    return Ok();
                }

                _logService.LogWarning("Tentativa de exclusão de uma cultura com ID inválido"); // Registre um aviso de log
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir Cultura: {ex.Message}"); // Registre um erro de log
                return StatusCode(StatusCodes.Status500InternalServerError, $"Cultura delete - {ex.Message}");
            }
        }
    }
}

