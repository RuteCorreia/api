using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _loggerService;

        public ClienteController(IClienteService clienteService, LoggedUserInfoService loggedUserInfoService, ILogService loggerService)
        {
            _clienteService = clienteService;
            _loggedUserInfoService = loggedUserInfoService;
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<ClienteViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var clientes = await _clienteService.GetAllAsync(loggedUser.Item3);
                _loggerService.LogInformation("Todos os clientes foram recuperados com sucesso.");
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar todos os clientes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar todos os clientes: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteViewModel>> GetById(int id)
        {
            var returnMsg = new StringBuilder().Append("Não encontrado");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var cliente = await _clienteService.GetByIdAsync(id, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(cliente))
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(cliente) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar o cliente com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar cliente por ID: {ex.Message}").ToString());
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClienteViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    await _clienteService.AddAsync(obj, loggedUser.Item3);
                    returnMsg.Clear();
                    _loggerService.LogInformation("Cliente adicionado com sucesso.");
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao adicionar cliente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao adicionar cliente: {ex.Message}").ToString());
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] ClienteViewModel obj)
        {
            var returnMsg = new StringBuilder().Append("Modelo inválido");
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var objeto = await _clienteService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.IdCliente = objeto.IdCliente;

                        await _clienteService.UpdateAsync(obj);
                        returnMsg.Clear();
                        _loggerService.LogInformation($"Cliente com ID {id} atualizado com sucesso.");
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
                _loggerService.LogError(ex, $"Erro ao atualizar cliente com ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao atualizar cliente: {ex.Message}").ToString());
            }
        }

        [HttpGet("getByName/{name}")]
        public async Task<ActionResult<IAsyncEnumerable<ClienteViewModel>>> GetByName(string name)
        {
            var returnMsg = new StringBuilder().Append("Não encontrados");
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var cliente = await _clienteService.GetByNameAsync(name, loggedUser.Item3);
                if (!ObjectNullValidation.IsObjectNull(cliente))
                    returnMsg.Clear();

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(cliente) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao buscar os clientes com Nome {name}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao buscar clientes por Nome: {ex.Message}").ToString());
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
                    await _clienteService.DeleteAsync(id, loggedUser.Item3);
                    returnMsg.Clear();
                    _loggerService.LogInformation($"Cliente com ID {id} deletado com sucesso.");
                }

                return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex, $"Erro ao deletar cliente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Erro ao deletar cliente: {ex.Message}").ToString());
            }
        }
    }
}
