using Application.Application.Servicos.Cadastros.IdentificacaoAreaTratada;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.RelatorioAplicacao.Interface;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
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
    public class RelatorioAplicacaoController : ControllerBase
    {
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        private readonly IIdentificacaoAreaTratadaService _identificacaoAreaTratadaServiceService;
        private readonly IContratanteService _contratanteService;
        private readonly IAplicacaoRecomendacoesTecnicasService _aplicacaoRecomendacoesTecnicasService;
        private readonly ICaracteristicasProdutoAplicadoService _caracteristicasProdutoAplicadoService;
        private readonly IContratoPrestacaoServicoService _contratoPrestacaoServicoService;
        private readonly IDadosResponsavelService _dadosResponsavelService;
        private readonly ILogService _logService;
        private readonly LoggedUserInfoService _loggedUserInfoService;


        public RelatorioAplicacaoController(IRelatorioAplicacaoService relatorioAplicacaoService, ILogService logService,
                                            IIdentificacaoAreaTratadaService identificacaoAreaTratadaServiceService,
                                            IContratanteService contratanteService,
                                            IAplicacaoRecomendacoesTecnicasService aplicacaoRecomendacoesTecnicasService,
                                            ICaracteristicasProdutoAplicadoService caracteristicasProdutoAplicadoService,
                                            IContratoPrestacaoServicoService contratoPrestacaoServicoService,
                                            IDadosResponsavelService dadosResponsavelService,
                                            LoggedUserInfoService loggedUserInfoService)
        {
            _relatorioAplicacaoService = relatorioAplicacaoService;
            _logService = logService;
            _identificacaoAreaTratadaServiceService = identificacaoAreaTratadaServiceService;
            _contratanteService = contratanteService;
            _aplicacaoRecomendacoesTecnicasService = aplicacaoRecomendacoesTecnicasService;
            _caracteristicasProdutoAplicadoService = caracteristicasProdutoAplicadoService;
            _contratoPrestacaoServicoService = contratoPrestacaoServicoService;
            _dadosResponsavelService = dadosResponsavelService;
            _loggedUserInfoService = loggedUserInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<RelatorioAplicacaoViewModel>>> GetAll()
        {
            try
            {
                var relatorio = await _relatorioAplicacaoService.GetAllAsync();
                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RelatorioAplicacaoViewModel>> GetById(int id)
        {
            try
            {
                var relatorio = await _relatorioAplicacaoService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(relatorio))
                {
                    _logService.LogInformation("Relatório de aplicação recuperado com sucesso.");
                    return Ok(relatorio);
                }

                _logService.LogWarning("Relatório de aplicação não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Relatório de aplicação não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar relatório de aplicação pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar relatório de aplicação pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RelatorioAplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    obj.AplicacaoRecomendacoesTecnicas.IdAlturaVoo = null;
                    obj.AplicacaoRecomendacoesTecnicas.IdAeronave = null;
                    obj.AplicacaoRecomendacoesTecnicas.IdTipoDeProduto = null;
                    obj.AplicacaoRecomendacoesTecnicas.IdAplicacao = null;
                    obj.AplicacaoRecomendacoesTecnicas.IdEquipamento = null;
                    obj.AplicacaoRecomendacoesTecnicas.IdVeiculante = null;

                    var contratoPrestacaoServico = _contratoPrestacaoServicoService.AddAsync(obj.ContratoPrestacaoServico, "20");
                    var contratanteService = _contratanteService.AddAsync(obj.Contratante);
                    var identificacaoAreaTratadaServiceService = _identificacaoAreaTratadaServiceService.AddAsync(obj.IdentificacaoAreaTratada);
                    var aplicacaoRecomendacoesTecnicasService = _aplicacaoRecomendacoesTecnicasService.AddAsync(obj.AplicacaoRecomendacoesTecnicas);
                    var caracteristicasProdutoAplicadoService = _caracteristicasProdutoAplicadoService.AddAsync(obj.CaracteristicasProdutoAplicado, "20");
                    var dadosResponsavelService = _dadosResponsavelService.AddAsync(obj.DadosResponsavel, "20");

                    obj.ContratoPrestacaoServicoId = contratoPrestacaoServico.Result;
                    obj.ContratanteId = contratanteService.Result;
                    obj.IdentificacaoAreaTratadaId = identificacaoAreaTratadaServiceService.Result;
                    obj.RecomendacoesTecnicasId = aplicacaoRecomendacoesTecnicasService.Result;
                    obj.CaracteristicasProdutoAplicadoId = caracteristicasProdutoAplicadoService.Result;
                    obj.DadosResponsavelId = dadosResponsavelService.Result;
                    obj.Id = 0;

                    obj.AplicacaoRecomendacoesTecnicas = null;
                    obj.Contratante = null;
                    obj.IdentificacaoAreaTratada = null;
                    obj.CaracteristicasProdutoAplicado = null;
                    obj.ContratoPrestacaoServico = null;
                    obj.DadosResponsavel = null;

                    _relatorioAplicacaoService.AddAsync(obj);
                    _logService.LogInformation("Novo relatório de aplicação adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo relatório de aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar novo relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo relatório de aplicação: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] RelatorioAplicacaoViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _relatorioAplicacaoService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _relatorioAplicacaoService.UpdateAsync(obj);
                        _logService.LogInformation("Relatório de aplicação atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Relatório de aplicação não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Relatório de aplicação não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar relatório de aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar relatório de aplicação: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _relatorioAplicacaoService.DeleteAsync(id);
                    _logService.LogInformation("Relatório de aplicação deletado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para deletar relatório de aplicação.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar relatório de aplicação: {ex.Message}");
            }
        }
    }
}
