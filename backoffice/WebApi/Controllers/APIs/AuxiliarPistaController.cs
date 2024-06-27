using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.AuxiliarPista.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AuxiliarPistaController : ControllerBase
    {
        private readonly IAuxiliarPistaService _auxiliarPistaService;
        public AuxiliarPistaController(IAuxiliarPistaService auxiliarPistaService)
        {
            _auxiliarPistaService = auxiliarPistaService;
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AuxiliarPistaViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auxiliarPista = await _auxiliarPistaService.AddAuxiliarPistaAsync(obj);
                    return Ok(auxiliarPista);
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar recomendação técnica de aplicação: {ex.Message}");
            }
        }
    }
}
