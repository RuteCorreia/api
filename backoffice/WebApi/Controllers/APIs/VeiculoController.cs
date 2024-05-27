using Application.DTOs.Cadastros.Veiculo.Interface;
using Application.DTOs.Cadastros.Veiculo.ViewModel;
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
public class VeiculoController : ControllerBase
{
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IVeiculoService _veiculoService;

    public VeiculoController(
        LoggedUserInfoService loggedUserInfoService, 
        IVeiculoService veiculoService
    )
    {
        _loggedUserInfoService = loggedUserInfoService;
        _veiculoService = veiculoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<VeiculoViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var cadastros = await _veiculoService.GetAllAsync(loggedUser.Item3);
            return Ok(cadastros);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Veiculo getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VeiculoViewModel>> GetById(int id)
    {
        var returnMsg = new StringBuilder().Append("Não encontrado");
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var obj = await _veiculoService.GetByIdAsync(id, loggedUser.Item3);
            if (obj is not null)
                returnMsg.Clear();

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Veiculo getById - {ex.Message}").ToString());
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] VeiculoViewModel obj)
    {
        var returnMsg = new StringBuilder().Append("Modelo inválido");
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                await _veiculoService.AddAsync(obj, loggedUser.Item3);
                returnMsg.Clear();
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Veiculo add - {ex.Message}").ToString());
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] VeiculoViewModel obj)
    {
        var returnMsg = new StringBuilder().Append("Modelo inválido");
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var objeto = await _veiculoService.GetByIdAsync(id, loggedUser.Item3);
                if (objeto is not null)
                {
                    obj.Id = objeto.Id;
                    await _veiculoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Veiculo update - {ex.Message}").ToString());
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
                await _veiculoService.DeleteAsync(id, loggedUser.Item3);
                returnMsg.Clear();
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"Veiculo delete - {ex.Message}").ToString());
        }
    }
}
