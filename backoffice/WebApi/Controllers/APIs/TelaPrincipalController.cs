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
        public async Task<ActionResult> GetAllRelatoriosAeronave(DateTime? dataInicio, DateTime? dataFim)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var relatorios = await _relatorioAeronaveService.GetAllAsync(dataInicio,dataFim, loggedUser.Item3);
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
