using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
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
    public class CaracteristicasProdutoAplicadoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ICaracteristicasProdutoAplicadoService _caracteristicasProdutoAplicadoService;
        private readonly ILogService _loggerService;

        public CaracteristicasProdutoAplicadoController(
            LoggedUserInfoService loggedUserInfoService,
            ICaracteristicasProdutoAplicadoService caracteristicasProdutoAplicadoService,
            ILogService loggerService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _caracteristicasProdutoAplicadoService = caracteristicasProdutoAplicadoService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<CaracteristicasProdutoAplicadoViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var cadastros = await _caracteristicasProdutoAplicadoService.GetAllAsync(loggedUser.Item3);
                _loggerService.LogInformation("Todos os registros de CaracteristicasProdutoAplicado foram recuperados com sucesso.");
                return Ok(cadastros);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os registros de CaracteristicasProdutoAplicado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os registros de CaracteristicasProdutoAplicado: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoAplicadoViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                

                var obj = await _caracteristicasProdutoAplicadoService.GetByIdAsync(id, loggedUser.Item3);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao buscar a CaracteristicasProdutoAplicado com ID {id}: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar a CaracteristicasProdutoAplicado: {ex.Message}").ToString());
            }
        }

        [HttpGet("getForExportExcel/{id}")]
        public async Task<ActionResult<CaracteristicasProdutoAplicadoViewModel>> GetForExportExcel(int id)
        {
            try
            {
                var result = await _caracteristicasProdutoAplicadoService.GetForExportExcelAsync(id);
                if (!ObjectNullValidation.IsObjectNull(result))
                {
                    _loggerService.LogInformation($"Detalhes da identificação de área tratada com ID {id} obtidos com sucesso.");
                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao obter detalhes da identificação de área tratada com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter detalhes da identificação de área tratada com ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProdutoAplicadoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _caracteristicasProdutoAplicadoService.AddAsync(obj, loggedUser.Item3);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao adicionar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao adicionar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CaracteristicasProdutoAplicadoViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var objeto = await _caracteristicasProdutoAplicadoService.GetByIdAsync(id, loggedUser.Item3);
                    if (objeto is not null)
                    {
                        obj.Id = objeto.Id;
                        await _caracteristicasProdutoAplicadoService.UpdateAsync(obj);
                        returnMsg.Clear();
                    }
                    else
                    {
                        returnMsg.Clear().Append("Não encontrado");
                    }
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao atualizar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao atualizar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var returnMsg = new StringBuilder().Append("Solicitação não foi possível de ser executada");
            try
            {
                if (id != 0)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _caracteristicasProdutoAplicadoService.DeleteAsync(id, loggedUser.Item3);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao deletar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao deletar CaracteristicasProdutoAplicado: {ex.Message}").ToString());
            }
        }
    }
}
