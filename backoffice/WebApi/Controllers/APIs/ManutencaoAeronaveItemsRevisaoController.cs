using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs;

[Route("api/v1/[controller]")]
[ApiController]
//[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class ManutencaoAeronaveItemsRevisaoController : ControllerBase
{
    public ManutencaoAeronaveItemsRevisaoController()
    {
        
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<int>>> GetAll()
    {
        try
        {
            return Ok();
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Manutenção Aeronave Items Revisão getAll - {ex.Message}");
        }
    }
}
