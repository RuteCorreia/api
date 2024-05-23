using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Adjuvante.ViewModel;
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
public class ImportacaoPlanilhaController : ControllerBase
{
    private readonly IAdjuvanteService _adjuvanteService;

    public ImportacaoPlanilhaController(IAdjuvanteService adjuvanteService)
    {
        _adjuvanteService = adjuvanteService;
    }

    [HttpPost]
    public async Task<ActionResult> ImportarPlanilha([FromBody] string base64)
    {
        try
        {
            byte[] fileBytes = Convert.FromBase64String(base64);


            return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Adjuvante add - {ex.Message}");
        }
    }
}
