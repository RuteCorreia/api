using Application.DTOs.Cadastros.TelaPrincipal.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class TelaPrincipalController : ControllerBase
    {
        private readonly IRelatorioAeronaveService _relatorioAeronaveService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        public TelaPrincipalController(
            IRelatorioAeronaveService relatorioAeronaveService,
            LoggedUserInfoService loggedUserInfoService)
        {
            _relatorioAeronaveService = relatorioAeronaveService; 
            _loggedUserInfoService = loggedUserInfoService;
        }


        [HttpGet("GetAllRelatoriosAeronave")]
        public async Task<ActionResult> GetAllRelatoriosAeronave([FromQuery] string? dataFiltro)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    DateTime? data = null;
                    if (!string.IsNullOrEmpty(dataFiltro))
                    {
                        // Tenta converter o dataFiltro para DateTime usando o formato "yyyy-MM-dd"
                        if (DateTime.TryParseExact(dataFiltro, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            data = parsedDate;
                        }
                        else
                        {
                            return BadRequest("Formato de data inválido. Use o formato yyyy-MM-dd.");
                        }
                    }
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var relatorios = await _relatorioAeronaveService.GetAllAsync(data, loggedUser.Item3);
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
