using Application.Application.Servicos.Cadastros.TipoDeServico;
using Application.DTOs.Cadastros.Sincronizacao.Interface;
using Application.DTOs.Cadastros.Sincronizacao.ViewModel;
using Application.DTOs.Cadastros.TipoDeServico.Interface;
using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.Interface;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;
using Application.DTOs.Log.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.HttpRequestInfo;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class SincronizacaoController : ControllerBase
    {
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly ISincronizacaoService _sincronizacaoService;
        private readonly ILogService _logService;

        public SincronizacaoController(
            LoggedUserInfoService loggedUserInfoService,
            ISincronizacaoService sincronizacaoService, 
            ILogService logService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _sincronizacaoService = sincronizacaoService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<SincronizacaoViewModel>> GetAll(DateTime dataUltimaSincronizacao)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var sincronizacao = await _sincronizacaoService.GetAsync(loggedUser.Item3, dataUltimaSincronizacao);
                _logService.LogInformation("Consulta dos dados para sincronização feita com sucesso");
                return Ok(sincronizacao);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar dados para sincronização: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar dados para sincronização: {ex.Message}");
            }
        }


    }
}
