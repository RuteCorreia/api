using Application.DTOs.Cadastros.TelaPrincipal.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelaPrincipalController : ControllerBase
    {
        private readonly IRelatorioAeronaveService _relatorioAeronaveService;
        public TelaPrincipalController(IRelatorioAeronaveService relatorioAeronaveService)
        {
            _relatorioAeronaveService = relatorioAeronaveService; 
        }


        [HttpGet("GetAllRelatoriosAeronave")]
        public async Task<ActionResult> GetAllRelatoriosAeronave()
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var relatorios = await _relatorioAeronaveService.GetAllAsync();
                    return Ok(relatorios);
                }

                return BadRequest("Modelo inválido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar campo IsMapa do relatório de aplicação: {ex.Message}");
            }
        }
    }
}
