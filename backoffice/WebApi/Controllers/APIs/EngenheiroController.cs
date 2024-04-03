using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;
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
public class EngenheiroController : ControllerBase
{
    private readonly IEngenheiroService _engenheiroService;

    public EngenheiroController(IEngenheiroService engenheiroService)
    {
        _engenheiroService = engenheiroService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<EngenheiroViewModel>>> GetAll()
    {
        try
        {
            var engenheiros = await _engenheiroService.GetAllAsync();
            return Ok(engenheiros);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Engenheiro getAll - {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngenheiroViewModel>> GetById(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var engenheiro = await _engenheiroService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(engenheiro))
                {
                    return Ok(engenheiro);
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Engenheiro getById - {ex.Message}");
        }
    }
}
