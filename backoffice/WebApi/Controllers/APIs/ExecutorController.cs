using Application.DTOs.Cadastros.Executor.Interface;
using Application.DTOs.Cadastros.Executor.ViewModel;
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
public class ExecutorController : ControllerBase
{
    private readonly IExecutorService _executorService;

    public ExecutorController(IExecutorService executorService)
    {
        _executorService = executorService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<ExecutorViewModel>>> GetAll()
    {
        try
        {
            var executores = await _executorService.GetAllAsync();
            return Ok(executores);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Executor getAll - {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExecutorViewModel>> GetById(string id)
    {
        try
        {
            if(!string.IsNullOrEmpty(id))
            {
                var executor = await _executorService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(executor))
                {
                    return Ok(executor);
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, "Não encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Executor getById - {ex.Message}");
        }
    }
}