using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Log.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public RelatorioAplicacaoController(
            IRelatorioAplicacaoService relatorioAplicacaoService,
            ILogService logService,
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
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetAll()
        {
            try
            {
                var relatorios = await _relatorioAplicacaoService.GetAllAsync();
                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorios);
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
                if (relatorio == null)
                {
                    _logService.LogWarning("Relatório de aplicação não encontrado.");
                    return NotFound("Relatório de aplicação não encontrado");
                }

                _logService.LogInformation("Relatório de aplicação recuperado com sucesso.");
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar relatório de aplicação pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar relatório de aplicação pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] dynamic obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logService.LogInformation("Received object: " + JsonConvert.SerializeObject(obj));

                    var id = (int)obj.id;
                    var contratanteJson = (string)obj.contratante;
                    var identificacaoAreaTratadaJson = (string)obj.identificacaoAreaTratada;
                    var caracteristicasProdutoAplicadoJson = (string)obj.caracteristicasProdutoAplicado;
                    var recomendacoesTecnicasJson = (string)obj.recomendacoesTecnicas;
                    var relatorioAplicacaoJson = (string)obj.relatorioAplicacao;
                    var contratoPrestacaoServicoJson = (string)obj.contratoPrestacaoServico;
                    var dadosResponsavelJson = (string)obj.dadosResponsavel;
                    var piloto = (string)obj.piloto;
                    var executor = (string)obj.executor;
                    var refDocument = (string)obj.refDocument;
                    var data = (string)obj.data;
                    var refUsuario = (string)obj.refUsuario;

                    var contratanteViewModel = JsonConvert.DeserializeObject<ContratanteViewModel>(contratanteJson);
                    var identificacaoAreaTratada = JsonConvert.DeserializeObject<IdentificacaoAreaTratadaViewModel>(identificacaoAreaTratadaJson);
                    var identificacaoAreaTratadaViewModel = JsonConvert.DeserializeObject<CaracteristicasProdutoAplicadoViewModel>(caracteristicasProdutoAplicadoJson);
                    var aplicacaoRecomendacoesTecnicasViewModel = JsonConvert.DeserializeObject<AplicacaoRecomendacoesTecnicasViewModel>(recomendacoesTecnicasJson);
                    var relatorioAplicacaoViewModel = JsonConvert.DeserializeObject<RelatorioAplicacaoViewModel>(relatorioAplicacaoJson);
                    var contratoPrestacaoServicoViewModel = JsonConvert.DeserializeObject<ContratoPrestacaoServicoViewModel>(contratoPrestacaoServicoJson);
                    var dadosResponsavelViewModel = JsonConvert.DeserializeObject<DadosResponsavelViewModel>(dadosResponsavelJson);

                    _logService.LogInformation($"Id: {id}, Piloto: {piloto}, Executor: {executor}");

                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    if (aplicacaoRecomendacoesTecnicasViewModel != null)
                    {
                        aplicacaoRecomendacoesTecnicasViewModel.IdAlturaVoo = null;
                        aplicacaoRecomendacoesTecnicasViewModel.IdAeronave = null;
                        aplicacaoRecomendacoesTecnicasViewModel.IdTipoDeProduto = null;
                        aplicacaoRecomendacoesTecnicasViewModel.IdAplicacao = null;
                        aplicacaoRecomendacoesTecnicasViewModel.IdEquipamento = null;
                        aplicacaoRecomendacoesTecnicasViewModel.IdVeiculante = null;
                    }

                    var IdcontratoPrestacaoServico = await _contratoPrestacaoServicoService.AddAsync(contratoPrestacaoServicoViewModel, loggedUser.Item3);
                    var Idcontratante = await _contratanteService.AddAsync(contratanteViewModel);
                    var IdIdentificacaoAreaTratada = await _identificacaoAreaTratadaServiceService.AddAsync(identificacaoAreaTratada);
                    var IdrecomendacoesTecnicas = aplicacaoRecomendacoesTecnicasViewModel != null ? await _aplicacaoRecomendacoesTecnicasService.AddAsync(aplicacaoRecomendacoesTecnicasViewModel) : (int?)null;
                    var IdcaracteristicasProdutoAplicado = await _caracteristicasProdutoAplicadoService.AddAsync(identificacaoAreaTratadaViewModel, loggedUser.Item3);
                    var IdDadosResponsavel = await _dadosResponsavelService.AddAsync(dadosResponsavelViewModel, loggedUser.Item3);

                    relatorioAplicacaoViewModel.ContratoPrestacaoServicoId = IdcontratoPrestacaoServico;
                    relatorioAplicacaoViewModel.ContratanteId = Idcontratante;
                    relatorioAplicacaoViewModel.IdentificacaoAreaTratadaId = IdIdentificacaoAreaTratada;
                    relatorioAplicacaoViewModel.RecomendacoesTecnicasId = IdrecomendacoesTecnicas;
                    relatorioAplicacaoViewModel.CaracteristicasProdutoAplicadoId = IdcaracteristicasProdutoAplicado;
                    relatorioAplicacaoViewModel.DadosResponsavelId = IdDadosResponsavel;
                    relatorioAplicacaoViewModel.Id = 0;
                    relatorioAplicacaoViewModel.Piloto = piloto;
                    relatorioAplicacaoViewModel.Executor = executor;
                    relatorioAplicacaoViewModel.RefDocument = refDocument;
                    relatorioAplicacaoViewModel.Data = data;
                    relatorioAplicacaoViewModel.RefUsuario = refUsuario;

                    var relatorioAplicacaoId = await _relatorioAplicacaoService.AddAsync(relatorioAplicacaoViewModel);

                    _logService.LogInformation("Novo relatório de aplicação adicionado com sucesso.");

                    var result = new
                    {
                        id = relatorioAplicacaoId,
                        contratanteId = Idcontratante,
                        identificacaoAreaTratadaId = IdIdentificacaoAreaTratada,
                        caracteristicasProdutoAplicadoId = IdcaracteristicasProdutoAplicado,
                        recomendacoesTecnicasId=IdrecomendacoesTecnicas,
                        relatorioAplicacao = new
                        {
                            id = relatorioAplicacaoId,
                            aplicacoes = new int[] { 1, 2 }
                        },
                        contratoPrestacaoServicoId = IdcontratoPrestacaoServico,
                        dadosResponsavelId= IdDadosResponsavel
                    };

                    return Ok(result);
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo relatório de aplicação.");
                return BadRequest("Modelo inválido");
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
                    var relatorioExistente = await _relatorioAplicacaoService.GetByIdAsync(id);
                    if (relatorioExistente == null)
                    {
                        _logService.LogWarning("Relatório de aplicação não encontrado para atualização.");
                        return NotFound("Relatório de aplicação não encontrado");
                    }

                    obj.Id = relatorioExistente.Id;
                    await _relatorioAplicacaoService.UpdateAsync(obj);
                    _logService.LogInformation("Relatório de aplicação atualizado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao atualizar relatório de aplicação.");
                return BadRequest("Modelo inválido");
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
                if (id == 0)
                {
                    _logService.LogWarning("Solicitação inválida para deletar relatório de aplicação.");
                    return BadRequest("Solicitação não foi possível de ser executada");
                }

                await _relatorioAplicacaoService.DeleteAsync(id);
                _logService.LogInformation("Relatório de aplicação deletado com sucesso.");
                return Ok();
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar relatório de aplicação: {ex.Message}");
            }
        }
    }
}
