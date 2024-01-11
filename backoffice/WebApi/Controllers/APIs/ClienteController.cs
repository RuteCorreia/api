using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.Cliente.ViewModel;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

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

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet("clientes")]
    public async Task<ActionResult<IAsyncEnumerable<ClienteViewModel>>> GetAll()
    {
        try
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente getAll - {ex.Message}");
        }
    }

    [HttpGet("GetClienteById")]
    public async Task<ActionResult<ClienteViewModel>> GetById(int id)
    {
        try
        {
            var cliente = await _clienteService.GetByIdAsync(id);
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

    [HttpPost("CriarCliente")]
    public async Task<ActionResult> Add([FromBody] ClienteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _clienteService.AddAsync(obj);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Cliente add - {ex.Message}");
        }
    }

    [HttpPost("UpdateCliente")]
    public async Task<ActionResult> Update(int id, [FromBody] ClienteViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _clienteService.GetByIdAsync(id);
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

    [HttpDelete("RemoveCliente")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _clienteService.DeleteAsync(id);
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
