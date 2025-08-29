using Application.Application.Servicos.Cadastros.ControleDeFrota;
using Application.Application.Servicos.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Sincronizacao.Interface;
using Application.DTOs.Cadastros.Sincronizacao.ViewModel;
using Application.DTOs.Log.Interface;
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
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        private readonly IControleDeFrotaService _controleDeFrotaService;
        private readonly ILogService _logService;

        public SincronizacaoController(
            LoggedUserInfoService loggedUserInfoService,
            ISincronizacaoService sincronizacaoService,
            IRelatorioAplicacaoService relatorioAplicacaoService,
            IControleDeFrotaService controleDeFrotaService,
            ILogService logService)
        {
            _loggedUserInfoService = loggedUserInfoService;
            _sincronizacaoService = sincronizacaoService;
            _relatorioAplicacaoService = relatorioAplicacaoService;
            _controleDeFrotaService = controleDeFrotaService;
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

        [HttpPost("getRelatorios")]
        public async Task<ActionResult<SincronizacaoRelatorioViewModel>> GetReports([FromBody] SincronizacaoRelatorioParams relatorioParams)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var response = new SincronizacaoRelatorioViewModel();
                response.RelatoriosAplicacao = await _relatorioAplicacaoService.GetListByIdsAsync(relatorioParams.IdsAplicacao, loggedUser.Item3);
                response.RelatoriosFrota = await _controleDeFrotaService.GetListByIdsAsync(relatorioParams.IdsFrota, loggedUser.Item3);

                _logService.LogInformation("Consulta de atualização dos relatórios feita com sucesso");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar dados para sincronização: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar dados para sincronização: {ex.Message}");
            }
        }
    }
}
