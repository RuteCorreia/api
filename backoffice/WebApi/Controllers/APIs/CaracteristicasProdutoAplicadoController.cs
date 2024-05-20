using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
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
public class CaracteristicasProdutoAplicadoController : ControllerBase
{
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly ICaracteristicasProdutoAplicadoService _caracteristicasProdutoAplicadoService;
    public CaracteristicasProdutoAplicadoController(
        LoggedUserInfoService loggedUserInfoService,
        ICaracteristicasProdutoAplicadoService caracteristicasProdutoAplicadoService
    )
    {
        _loggedUserInfoService = loggedUserInfoService;
        _caracteristicasProdutoAplicadoService = caracteristicasProdutoAplicadoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<CaracteristicasProdutoAplicadoViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var cadastros = await _caracteristicasProdutoAplicadoService.GetAllAsync(loggedUser.Item3);
            return Ok(cadastros);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"CaracteristicasProdutoAplicado getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CaracteristicasProdutoAplicadoViewModel>> GetById(int id)
    {
        var returnMsg = new StringBuilder().Append("Não encontrado");
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var cliente = await _caracteristicasProdutoAplicadoService.GetByIdAsync(id, loggedUser.Item3);
            if (cliente is not null)
                returnMsg.Clear();

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(cliente) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"CaracteristicasProdutoAplicado getById - {ex.Message}").ToString());
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CaracteristicasProdutoAplicadoViewModel obj)
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"CaracteristicasProdutoAplicado add - {ex.Message}").ToString());
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"CaracteristicasProdutoAplicado update - {ex.Message}").ToString());
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"CaracteristicasProdutoAplicado delete - {ex.Message}").ToString());
        }
    }
}
