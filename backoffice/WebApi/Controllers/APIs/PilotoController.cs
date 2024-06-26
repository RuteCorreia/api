using Application.DTOs.Cadastros.Piloto.Interface;
using Application.DTOs.Cadastros.Piloto.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoService _pilotoService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ILogService _logService;

        public PilotoController(
            IPilotoService pilotoService,
            LoggedUserInfoService loggedUserInfoService,
            ILogService logService
        )
        {
            _pilotoService = pilotoService;
            _loggedUserInfoService = loggedUserInfoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PilotoViewModel>>> GetAll()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var pilotos = await _pilotoService.GetAllAsync(loggedUser.Item3);
                _logService.LogInformation("Pilotos recuperados com sucesso.");
                return Ok(pilotos);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar pilotos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar pilotos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PilotoViewModel>> GetById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    var piloto = await _pilotoService.GetByIdAsync(id, loggedUser.Item3);
                    if (!ObjectNullValidation.IsObjectNull(piloto))
                    {
                        _logService.LogInformation("Piloto recuperado com sucesso.");
                        return Ok(piloto);
                    }
                }

                _logService.LogWarning("Piloto não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Piloto não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar piloto pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar piloto pelo ID: {ex.Message}");
            }
        }
    }
}
