using Application.DTOs.Cadastros.Menu.Interface;
using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.SubMenu.Interface;
using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

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
    private readonly IMapper _mapper;

    public MenuController(IMenuService menuService, ISubMenuService subMenuService, IMapper mapper)
    {
        _menuService = menuService;
        _subMenuService = subMenuService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<MenuViewModel>> GetAll()
    {
        try
        {
            var menu = _menuService.GetAllAsync();
            var subMenu = _subMenuService.GetAllAsync();

            foreach (var m in menu)
            {
                foreach (var submenu in subMenu)
                {
                    if(m.MenuItemId == submenu.MenuItemId)
                    {
                        m.SubMenus.Add(submenu);

                    }
                }

            }

            return Ok(menu);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu getAll - {ex.Message}");
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

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu getById - {ex.Message}");
        }
    }

    [HttpGet("dropdown")]
    public async Task<ActionResult> GetDropdown()
    {
        try
        {
            var menu = _menuService.GetAllAsync();
            var subMenu = _subMenuService.GetAllAsync();

            foreach(var submenu in subMenu)
            {
                var newObj = new MenuViewModel
                {
                    MenuItemId = submenu.SubMenuId,
                    Label = submenu.Label,
                };

                menu = menu.Concat(new[] { newObj });
            }

            return Ok(menu);
        }
        catch(Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu dropdown - {ex.Message}");
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
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu add - {ex.Message}");
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _menuService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Menu delete - {ex.Message}");
        }
    }
}
