using Application.DTOs.Cadastros.MenuUsuario.Interface;
using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
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
public class MenuUsuarioController : ControllerBase
{
    private readonly IMenuUsuarioService _menuUsuarioService;

    public MenuUsuarioController(IMenuUsuarioService menuUsuarioService)
    {
        _menuUsuarioService = menuUsuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<MenuUsuarioViewModel>>> GetAll()
    {
        try
        {
            var menuUsuario = await _menuUsuarioService.GetAllAsync();
            return Ok(menuUsuario);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"MenuUsuario getAll - {ex.Message}");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MenuUsuarioViewModel>> GetById(string id)
    {
        try
        {
            var menuUsuario = await _menuUsuarioService.GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(menuUsuario))
            {
                return Ok(menuUsuario);
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"MenuUsuario getById - {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] MenuUsuarioViewModel obj)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _menuUsuarioService.AddAsync(obj);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"MenuUsuario add - {ex.Message}");
        }
    }

    //[HttpPut("{id:int}")]
    //public async Task<ActionResult> Update(int id, [FromBody] MenuUsuarioViewModel obj)
    //{
    //    try
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var objeto = await _menuUsuarioService.GetByIdAsync(id);
    //            if (!ObjectNullValidation.IsObjectNull(objeto))
    //            {
    //                obj.Id = objeto.id;

    //                await _menuService.UpdateAsync(obj);
    //                return Ok();
    //            }
    //            else
    //            {
    //                return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
    //            }
    //        }

    //        return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError, $"Menu update - {ex.Message}");
    //    }
    //}

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            if (id != 0)
            {
                await _menuUsuarioService.DeleteAsync(id);
                return Ok();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"MenuUsuario delete - {ex.Message}");
        }
    }
}
