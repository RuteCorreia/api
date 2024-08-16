using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.Atividade.Interface;
using Application.DTOs.Cadastros.Atividade.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AtividadeController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IAtividadeService _atividadeService;
        public AtividadeController(
            LoggedUserInfoService loggedUserInfoService,
            IAtividadeService atividadeService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _atividadeService = atividadeService;
        }

        [HttpPost("GetAtividadesByFiltros")]
        public async Task<ActionResult<AtividadeViewModel>> GetAtividadesByFiltros([FromBody] AtividadeFiltroViewModel atividadeFiltro)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var atividade = await _atividadeService.GetAtividadeByFiltrosAsync(atividadeFiltro, loggedUser.Item3);
                return Ok(atividade);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"AlvoBiologico getAll - {ex.Message}");
            }
        }
    }
}
