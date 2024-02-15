using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
using Application.DTOs.Cadastros.Menu.Interface;
using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.SubMenu.Interface;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
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
public class SubMenuController : ControllerBase
{
    private readonly ISubMenuService _subMenuService;

    public SubMenuController(ISubMenuService subMenuService)
    {
        _subMenuService = subMenuService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<SubMenuViewModel>>> GetAll()
    {
        try
        {
            var subMenu = await _subMenuService.GetAllAsync();
            return Ok(subMenu);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"SubMenu getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubMenuViewModel>> GetById(int id)
    {
        try
        {
            var subMenu = await _subMenuService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(subMenu))
            {
                return Ok(subMenu);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"SubMenu getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] SubMenuViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _subMenuService.AddAsync(obj);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"SubMenu add - {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] SubMenuViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var objeto = await _subMenuService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(objeto))
                {
                    obj.SubMenuId = objeto.SubMenuId;

                    await _subMenuService.UpdateAsync(obj);
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
            return StatusCode(StatusCodes.Status500InternalServerError, $"SubMenu update - {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _subMenuService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"SubMenu delete - {ex.Message}");
        }
    }
}
