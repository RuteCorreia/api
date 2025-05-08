using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.ProdutoAplicado.Interface;
using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
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
    public class ProdutoAplicadoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IProdutoAplicadoService _produtoAplicadoService;
        private readonly ILogService _loggerService;

        public ProdutoAplicadoController(
            LoggedUserInfoService loggedUserInfoService,
            IProdutoAplicadoService produtoAplicadoService,
            ILogService loggerService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _produtoAplicadoService = produtoAplicadoService;
            _loggerService = loggerService;
        }

        [HttpGet("{idCaracteristicasProdutoAplicado}")]
        public async Task<ActionResult<IAsyncEnumerable<ProdutoAplicadoCaracteristicasViewModel>>> GetAllByIdCaracteristicaProdutoAplicadoAsync(int idCaracteristicasProdutoAplicado)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();


                var obj = await _produtoAplicadoService.GetAllByIdCaracteristicaProdutoAplicadoAsync(idCaracteristicasProdutoAplicado);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao buscar produtos da CaracteristicasProdutoAplicado ID {idCaracteristicasProdutoAplicado}: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar a produtos aplicados: {ex.Message}").ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoAplicadoCaracteristicasViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                

                var obj = await _produtoAplicadoService.GetByIdAsync(id);
                if (obj is not null)
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao buscar a ProdutoAplicado com ID {id}: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar a ProdutoAplicado: {ex.Message}").ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProdutoAplicadoCaracteristicasViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    await _produtoAplicadoService.AddAsync(obj);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao adicionar ProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao adicionar ProdutoAplicado: {ex.Message}").ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProdutoAplicadoCaracteristicasViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _produtoAplicadoService.GetByIdAsync(id);
                    if (objeto is not null)
                    {
                        obj.Id = objeto.Id;
                        await _produtoAplicadoService.UpdateAsync(obj);
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
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao atualizar ProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao atualizar ProdutoAplicado: {ex.Message}").ToString());
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
                    await _produtoAplicadoService.DeleteAsync(id);
                    returnMsg.Clear();
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, returnMsg.Clear().Append($"Erro ao deletar ProdutoAplicado: {ex.Message}").ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao deletar ProdutoAplicado: {ex.Message}").ToString());
            }
        }
    }
}
