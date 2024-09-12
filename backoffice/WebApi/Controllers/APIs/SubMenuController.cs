using Application.DTOs.Cadastros.SubMenu.Interface;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Administrador")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class SubMenuController : ControllerBase
{
    private readonly ISubMenuService _subMenuService;
    private readonly ILogService _logService;
    private readonly LoggedUserInfoService _loggedUserInfoService;

    public SubMenuController(
        ISubMenuService subMenuService,
        ILogService logService,
        LoggedUserInfoService loggedUserInfoService
    )
    {
        _subMenuService = subMenuService;
        _logService = logService;
        _loggedUserInfoService = loggedUserInfoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<SubMenuViewModel>>> GetAll()
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                var subMenu = await _subMenuService.GetAllAsync();
                _logService.LogInformation("Todos os submenus foram recuperados com sucesso.");
                return Ok(subMenu);
            }

            _logService.LogWarning($"Acesso não autorizado ao gerenciamento de submenus pelo usuário {loggedUser.Item1}");
            return Unauthorized("Não permitido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar todos os submenus: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os submenus: {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubMenuViewModel>> GetById(int id)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                var subMenu = await _subMenuService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(subMenu))
                {
                    _logService.LogInformation("SubMenu recuperado com sucesso.");
                    return Ok(subMenu);
                }

                _logService.LogWarning("SubMenu não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "SubMenu não encontrado");
            }

            _logService.LogWarning($"Acesso não autorizado ao detalhe de submenus pelo usuário {loggedUser.Item1}");
            return Unauthorized("Não permitido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao recuperar SubMenu pelo ID: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar SubMenu pelo ID: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] SubMenuViewModel obj)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (ModelState.IsValid)
            {
                await _subMenuService.AddAsync(obj);
                _logService.LogInformation("Novo SubMenu adicionado com sucesso.");
                return Ok();
            }

            _logService.LogWarning("Modelo inválido ao adicionar novo SubMenu.");
            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");

            _logService.LogWarning($"Acesso não autorizado à criação de submenus pelo usuário {loggedUser.Item1}");
            return Unauthorized("Não permitido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao adicionar novo SubMenu: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo SubMenu: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] SubMenuViewModel obj)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _subMenuService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.SubMenuId = objeto.SubMenuId;

                        await _subMenuService.UpdateAsync(obj);
                        _logService.LogInformation("SubMenu atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("SubMenu não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "SubMenu não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar SubMenu.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }

            _logService.LogWarning($"Acesso não autorizado à atualização de submenus pelo usuário {loggedUser.Item1}");
            return Unauthorized("Não permitido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao atualizar SubMenu: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar SubMenu: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
            if (string.IsNullOrEmpty(loggedUser.Item3))
            {
                if (id != 0)
                {
                    await _subMenuService.DeleteAsync(id);
                    _logService.LogInformation("SubMenu deletado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para deletar SubMenu.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }

            _logService.LogWarning($"Acesso não autorizado à remoção de submenus pelo usuário {loggedUser.Item1}");
            return Unauthorized("Não permitido");
        }
        catch (Exception ex)
        {
            _logService.LogError(ex, $"Erro ao deletar SubMenu: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar SubMenu: {ex.Message}");
        }
    }
}
