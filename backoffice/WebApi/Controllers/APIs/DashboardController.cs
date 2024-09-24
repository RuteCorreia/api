using Application.DTOs.Cadastros.Dashboard.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            LoggedUserInfoService loggedUserInfoService,
            IDashboardService dashboardService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(DateTime? dataInicio, DateTime? dataFim, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var result = await _dashboardService.GetAllAsync(dataInicio,dataFim,loggedUser.Item3,usuario,nomeAeronave,nomeContratante);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }
    }
}
