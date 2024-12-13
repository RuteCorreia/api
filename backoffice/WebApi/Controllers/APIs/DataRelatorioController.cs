using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DataRelatorioController : ControllerBase
    {
        
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDataRelatorioService _dataRelatorioService;
        private readonly ILogService _logService;

        public DataRelatorioController(
            LoggedUserInfoService loggedUserInfoService,
            IDataRelatorioService dataRelatorioService,
            ILogService logService)
        {
            _dataRelatorioService = dataRelatorioService;
            _loggedUserInfoService = loggedUserInfoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<DataRelatorioViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var dadoRelatorio = await _dataRelatorioService.GetAllAsync(loggedUser.Item3);
                _logService.LogInformation("Lista de todas as Data relatorio obtida com sucesso.");
                return Ok(dadoRelatorio);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter todas as Data relatorio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter todas as Data relatorio: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataRelatorioViewModel>> GetById(int id)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var dadoRelatorio = await _dataRelatorioService.GetByIdAsync(id,loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(dadoRelatorio))
                {
                    _logService.LogInformation($"Detalhes da Data relatorio com ID {id} obtidos com sucesso.");
                    return Ok(dadoRelatorio);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao obter detalhes da Data relatorio com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes da Data relatorio com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DataRelatorioViewModel obj)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var Id = await _dataRelatorioService.AddAsync(obj, loggedUser.Item3);
                    _logService.LogInformation("Data relatorio adicionada com sucesso.");
                    return Ok(Id);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar Data relatorio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar Data relatorio: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] DataRelatorioViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var existingObj = await _dataRelatorioService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(existingObj))
                    {
                        obj.Id = existingObj.Id;
                        var retId = await _dataRelatorioService.UpdateAsync(obj);
                        _logService.LogInformation($"Data relatorio com ID {id} atualizada com sucesso.");
                        return Ok(retId);
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
                _logService.LogError(ex, $"Erro ao atualizar Data relatorio com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar Data relatorio com ID {id}: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _dataRelatorioService.DeleteAsync(id, loggedUser.Item3);
                    _logService.LogInformation($"Data relatorio com ID {id} excluída com sucesso.");
                    return Ok();
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao excluir Data relatorio com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir Data relatorio com ID {id}: {ex.Message}");
            }
        }
    }
}
