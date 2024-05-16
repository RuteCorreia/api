using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var cliente = await _clienteService.GetByIdAsync(id, loggedUser.Item3);
            if (!ObjectNullValidation.IsObjectNull(cliente))
            {
                return Ok(cliente);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ClienteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                await _clienteService.AddAsync(obj, loggedUser.Item3);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ClienteViewModel obj)
    {
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
                    return Ok();
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente update - {ex.Message}");
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
                await _clienteService.DeleteAsync(id, loggedUser.Item3);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente delete - {ex.Message}");
        }
    }
}
