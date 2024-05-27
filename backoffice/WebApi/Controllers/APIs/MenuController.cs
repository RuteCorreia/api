using Application.DTOs.Cadastros.Menu.Interface;
using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.SubMenu.Interface;
using Application.DTOs.Log.Interface;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ISubMenuService _subMenuService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _logService;

        public MenuController(IMenuService menuService, ISubMenuService subMenuService, LoggedUserInfoService loggedUserInfoService, ILogService logService)
        {
            _menuService = menuService;
            _subMenuService = subMenuService;
            _loggedUserInfoService = loggedUserInfoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<MenuViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var menu = await _menuService.GetAllAsync(loggedUser.Item3);
                var subMenu = await _subMenuService.GetAllAsync();

                foreach (var m in menu)
                {
                    foreach (var submenu in subMenu)
                    {
                        if (m.MenuItemId == submenu.MenuItemId)
                        {
                            m.SubMenus.Add(submenu);
                        }
                    }
                }

                _logService.LogInformation("Registros de menu recuperados com sucesso.");
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os registros de menu: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os registros de menu: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuViewModel>> GetById(int id)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(menu))
                {
                    return Ok(menu);
                }

                _logService.LogWarning("Menu não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Menu não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar menu pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar menu pelo ID: {ex.Message}");
            }
        }

        [HttpGet("dropdown")]
        public async Task<ActionResult> GetDropdown()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var menu = await _menuService.GetAllAsync(loggedUser.Item3);
                var subMenu = await _subMenuService.GetAllAsync();

                foreach (var submenu in subMenu)
                {
                    var newObj = new MenuViewModel
                    {
                        MenuItemId = submenu.SubMenuId,
                        Label = submenu.Label,
                    };

                    menu = menu.Concat(new[] { newObj });
                }

                _logService.LogInformation("Menu suspenso recuperado com sucesso.");
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar menu suspenso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar menu suspenso: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] MenuViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _menuService.AddAsync(obj);
                    _logService.LogInformation("Menu adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo de menu inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo de menu inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar menu: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar menu: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] MenuViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _menuService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.MenuItemId = objeto.MenuItemId;

                        await _menuService.UpdateAsync(obj);
                        _logService.LogInformation("Menu atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Menu não encontrado.");
                        return StatusCode(StatusCodes.Status404NotFound, "Menu não encontrado");
                    }
                }

                _logService.LogWarning("Modelo de menu inválido.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo de menu inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar menu: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar menu: {ex.Message}");
            }
        }
    }
}
