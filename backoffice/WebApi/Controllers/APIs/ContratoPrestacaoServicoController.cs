using Application.Application.Servicos.Cadastros.CaracteristicasProdutoAplicado;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
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
public class ContratoPrestacaoServicoController : ControllerBase
{
    private readonly LoggedUserInfoService _loggedUserInfoService;
    private readonly IContratoPrestacaoServicoService _contratoPrestacaoServicoService;

    public ContratoPrestacaoServicoController(
        LoggedUserInfoService loggedUserInfoService,
        IContratoPrestacaoServicoService contratoPrestacaoServicoService
    )
    {
        _contratoPrestacaoServicoService = contratoPrestacaoServicoService;
        _loggedUserInfoService = loggedUserInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ContratoPrestacaoServicoViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var cadastros = await _contratoPrestacaoServicoService.GetAllAsync(loggedUser.Item3);
            return Ok(cadastros);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"ContratoPrestacaoServico getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContratoPrestacaoServicoViewModel>> GetById(int id)
    {
        var returnMsg = new StringBuilder().Append("Não encontrado");
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            var obj = await _contratoPrestacaoServicoService.GetByIdAsync(id, loggedUser.Item3);
            if (obj is not null)
                returnMsg.Clear();

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok(obj) : StatusCode(StatusCodes.Status404NotFound, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"ContratoPrestacaoServico getById - {ex.Message}").ToString());
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ContratoPrestacaoServicoViewModel obj)
    {
        var returnMsg = new StringBuilder().Append("Modelo inválido");
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                await _contratoPrestacaoServicoService.AddAsync(obj, loggedUser.Item3);
                returnMsg.Clear();
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"ContratoPrestacaoServico add - {ex.Message}").ToString());
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] ContratoPrestacaoServicoViewModel obj)
    {
        var returnMsg = new StringBuilder().Append("Modelo inválido");
        try
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var objeto = await _contratoPrestacaoServicoService.GetByIdAsync(id, loggedUser.Item3);
                if (objeto is not null)
                {
                    obj.Id = objeto.Id;
                    await _contratoPrestacaoServicoService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"ContratoPrestacaoServico update - {ex.Message}").ToString());
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
                await _contratoPrestacaoServicoService.DeleteAsync(id, loggedUser.Item3);
                returnMsg.Clear();
            }

            return string.IsNullOrEmpty(returnMsg.ToString()) ? Ok() : StatusCode(StatusCodes.Status400BadRequest, returnMsg.ToString());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, returnMsg.Clear().Append($"ContratoPrestacaoServico delete - {ex.Message}").ToString());
        }
    }
}
