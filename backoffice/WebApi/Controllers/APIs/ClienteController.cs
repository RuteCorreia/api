using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly LoggedUserInfoService _loggedUserInfoService;

    public ClienteController(IClienteService clienteService, LoggedUserInfoService loggedUserInfoService)
    {
        _clienteService = clienteService;
        _loggedUserInfoService = loggedUserInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ClienteViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var clientes = await _clienteService.GetAllAsync(loggedUser.Item3);
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente getAll - {ex.Message}");
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Cliente getById - {ex.Message}").ToString());
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
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Cliente add - {ex.Message}").ToString());
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Cliente update - {ex.Message}").ToString());
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
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Cliente delete - {ex.Message}").ToString());
        }
    }
}
