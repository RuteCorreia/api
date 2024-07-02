using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.LocalIncendio.Interface;
using Application.DTOs.Cadastros.LocalIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioIncendio.Interface;
using Application.DTOs.Cadastros.RelatorioIncendio.ViewModel;
using Application.DTOs.Log.Interface;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.HttpRequestInfo;
using Application.DTOs.Cadastros.Pistas.ViewModel;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;
using Application.DTOs.Cadastros.Pistas.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Domain.Entidades.Cadastros.DadosResponsavel;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RelatorioIncendioController : ControllerBase
    {
        private readonly IRelatorioIncendioService _relatorioIncendioService;
        private readonly ILogService _logService;
        private readonly IPistaService _pistaService;
        private readonly ILocalIncendioService _localIncendioService;
        private readonly ICombateIncendioDecolagemPousoService _decolagemPousoFirefightingService;
        private readonly IDadosResponsavelService _dadosResponsavelService;
        private readonly IContratoPrestacaoServicoService _contratoPrestacaoServicoService;
        private readonly LoggedUserInfoService _loggedUserInfoService;


        public RelatorioIncendioController(IRelatorioIncendioService relatorioIncendioService, ILogService logService,
                                           IPistaService pistaService,
                                           ILocalIncendioService localIncendioService,
                                           ICombateIncendioDecolagemPousoService decolagemPousoFirefightingService,
                                           IDadosResponsavelService dadosResponsavelService,
                                           IContratoPrestacaoServicoService contratoPrestacaoServicoService,
                                           LoggedUserInfoService loggedUserInfoService)
        {
            _relatorioIncendioService = relatorioIncendioService;
            _logService = logService;
            _pistaService = pistaService;
            _localIncendioService = localIncendioService;
            _decolagemPousoFirefightingService = decolagemPousoFirefightingService;
            _dadosResponsavelService = dadosResponsavelService;
            _contratoPrestacaoServicoService = contratoPrestacaoServicoService;
            _loggedUserInfoService = loggedUserInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<RelatorioIncendioViewModel>>> GetAll()
        {
            try
            {
                var relatorio = await _relatorioIncendioService.GetAllAsync();
                _logService.LogInformation("Todos os relatórios de incêndio foram recuperados com sucesso.");
                return Ok(relatorio);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de incêndio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de incêndio: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RelatorioIncendioViewModel>> GetById(int id)
        {
            try
            {
                var relatorio = await _relatorioIncendioService.GetByIdAsync(id);
                if (!ObjectNullValidation.IsObjectNull(relatorio))
                {
                    _logService.LogInformation("Relatório de incêndio recuperado com sucesso.");
                    return Ok(relatorio);
                }

                _logService.LogWarning("Relatório de incêndio não encontrado.");
                return StatusCode(StatusCodes.Status404NotFound, "Relatório de incêndio não encontrado");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar relatório de incêndio pelo ID: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar relatório de incêndio pelo ID: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] dynamic obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logService.LogInformation("Received object: " + JsonConvert.SerializeObject(obj));

                    var id = obj.GetProperty("id").GetInt32();
                    var pistaJson = obj.GetProperty("pista").ToString();
                    var localIncendioJson = obj.GetProperty("localIncendio").ToString();
                    var decolagemPousoFirefightingJson = obj.GetProperty("decolagemPousoFirefighting").ToString();
                    var dadosResponsavelJson = obj.GetProperty("dadosResponsavel").ToString();
                    var coordenadorBaseOperacionalJson = obj.GetProperty("coordenadorBaseOperacional").ToString();
                    var comandanteOcorrenciaJson = obj.GetProperty("comandanteOcorrencia").ToString();
                    var contratoPrestacaoServicoJson = obj.GetProperty("contratoPrestacaoServico").ToString();

                    // Accessing properties dynamically
                    var pistaDeserializado = JsonConvert.DeserializeObject<PistaRelatorioIncendioViewModel>(pistaJson);
                    var localIncendioDeserializado = JsonConvert.DeserializeObject<LocalIncendioViewModel>(localIncendioJson);
                    var decolagemPousoFirefightingDeserializado = JsonConvert.DeserializeObject<List<CombateIncendioDecolagemPousoRelatorioIncendioViewModel>>(decolagemPousoFirefightingJson);
                    var dadosResponsavelDeserializado = JsonConvert.DeserializeObject<DadosResponsavelRelatorioIncendioViewModel>(dadosResponsavelJson);
                    var coordenadorBaseOperacionalDeserializado = JsonConvert.DeserializeObject<DadosResponsavelRelatorioIncendioViewModel>(coordenadorBaseOperacionalJson);
                    var comandanteOcorrenciaDeserializado = JsonConvert.DeserializeObject<DadosResponsavelRelatorioIncendioViewModel>(comandanteOcorrenciaJson);
                    var contratoPrestacaoServicoDeserializado = JsonConvert.DeserializeObject<ContratoPrestacaoServicoViewModel>(contratoPrestacaoServicoJson);

                    // Log some properties
                    _logService.LogInformation($"Id: {id}");

                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

                    #region Pista
                    dynamic? pista = new PistaViewModel();
                    pista.Nome = pistaDeserializado.NomePista;
                    pista.LAT = pistaDeserializado.LatPista;
                    pista.LONG = pistaDeserializado.LongPista;
                    var pistaService = _pistaService.AddAsync(pista);
                    #endregion

                    #region Local Incêndio
                    localIncendioDeserializado.Id = 0;
                    var localIncendioService = _localIncendioService.AddAsync(localIncendioDeserializado);
                    #endregion

                    #region Decolagem Pouso Firefighting
                    var decolagemPousoFirefightingIdLista = new List<int>();
                    foreach (var dpff in decolagemPousoFirefightingDeserializado)
                    {
                        dynamic? decolagemPousoFirefighting = new CombateIncendioDecolagemPousoViewModel();
                        decolagemPousoFirefighting.DecolagemHorario = dpff.HorarioDecolagem;
                        decolagemPousoFirefighting.DecolagemHorimetro = dpff.HorimetroDecolagem;
                        decolagemPousoFirefighting.PousoHorario = dpff.HorarioPouso;
                        decolagemPousoFirefighting.PousoHorimetro = dpff.HorimetroPouso;
                        var decolagemPousoFirefightingService = _decolagemPousoFirefightingService.AddAsync(decolagemPousoFirefighting, loggedUser.Item3);
                        decolagemPousoFirefightingIdLista.Add(decolagemPousoFirefightingService.Result);
                    }
                    #endregion

                    #region Dados Responsável
                    dynamic? dadosResponsavel = new DadosResponsavelViewModel();
                    dadosResponsavel.NomeCompleto = dadosResponsavelDeserializado.Nome;
                    dadosResponsavel.Documento = dadosResponsavelDeserializado.Documento;
                    dadosResponsavel.assinaturaResponsavel = dadosResponsavelDeserializado.Assinatura;
                    var dadosResponsavelService = _dadosResponsavelService.AddAsync(dadosResponsavel, loggedUser.Item3);
                    #endregion

                    #region Coordenador Base Operacional
                    int? coordenadorBaseOperacionalId = null;
                    if (coordenadorBaseOperacionalDeserializado.Id != 0)
                    {
                        dynamic? coordenadorBaseOperacional = new DadosResponsavelViewModel();
                        coordenadorBaseOperacional.NomeCompleto = coordenadorBaseOperacionalDeserializado.Nome;
                        coordenadorBaseOperacional.PostoGraduacao = coordenadorBaseOperacionalDeserializado.PostoGraduacao;
                        coordenadorBaseOperacional.Re = coordenadorBaseOperacionalDeserializado.Re;
                        coordenadorBaseOperacional.assinaturaResponsavel = coordenadorBaseOperacionalDeserializado.Assinatura;
                        var coordenadorBaseOperacionalService = _dadosResponsavelService.AddAsync(coordenadorBaseOperacional, loggedUser.Item3);
                        coordenadorBaseOperacionalId = coordenadorBaseOperacionalService.Result;
                    }
                    #endregion

                    #region Comandante Ocorrência
                    int? comandanteOcorrenciaId = null;
                    if (comandanteOcorrenciaDeserializado.Id != 0)
                    {
                        dynamic? comandanteOcorrencia = new DadosResponsavelViewModel();
                        comandanteOcorrencia.NomeCompleto = comandanteOcorrenciaDeserializado.Nome;
                        comandanteOcorrencia.PostoGraduacao = comandanteOcorrenciaDeserializado.PostoGraduacao;
                        comandanteOcorrencia.Re = comandanteOcorrenciaDeserializado.Re;
                        comandanteOcorrencia.assinaturaResponsavel = comandanteOcorrenciaDeserializado.Assinatura;
                        var comandanteOcorrenciaService = _dadosResponsavelService.AddAsync(comandanteOcorrencia, loggedUser.Item3);
                        comandanteOcorrenciaId = comandanteOcorrenciaService.Result;
                    }
                    #endregion

                    #region contrato Prestação Serviço
                    contratoPrestacaoServicoDeserializado.Id = 0;
                    contratoPrestacaoServicoDeserializado.UnidadePreco = contratoPrestacaoServicoDeserializado.Preco;
                    var contratoPrestacaoServicoService = _contratoPrestacaoServicoService.AddAsync(contratoPrestacaoServicoDeserializado, loggedUser.Item3);
                    #endregion

                    #region Relatório Incêndio
                    dynamic? relatorioIncendio = new RelatorioIncendioViewModel();
                    relatorioIncendio.PistaId = pistaService.Result;
                    relatorioIncendio.LocalIncendioId = localIncendioService.Result;
                    relatorioIncendio.DecolagemPousoFirefighting = decolagemPousoFirefightingIdLista;
                    relatorioIncendio.DadosResponsavelId = dadosResponsavelService.Result;
                    relatorioIncendio.CoordenadorBaseOperacionalId = coordenadorBaseOperacionalId;
                    relatorioIncendio.ComandanteOcorrenciaId = comandanteOcorrenciaId;
                    relatorioIncendio.ContratoPrestacaoServicoId = contratoPrestacaoServicoService.Result;
                    _relatorioIncendioService.AddAsync(relatorioIncendio);
                    #endregion

                    _logService.LogInformation("Novo relatório de incêndio adicionado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao adicionar novo relatório de incêndio.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao adicionar novo relatório de incêndio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar novo relatório de incêndio: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] RelatorioIncendioViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objeto = await _relatorioIncendioService.GetByIdAsync(id);
                    if (!ObjectNullValidation.IsObjectNull(objeto))
                    {
                        obj.Id = objeto.Id;

                        await _relatorioIncendioService.UpdateAsync(obj);
                        _logService.LogInformation("Relatório de incêndio atualizado com sucesso.");
                        return Ok();
                    }
                    else
                    {
                        _logService.LogWarning("Relatório de incêndio não encontrado para atualização.");
                        return StatusCode(StatusCodes.Status404NotFound, "Relatório de incêndio não encontrado");
                    }
                }

                _logService.LogWarning("Modelo inválido ao atualizar relatório de incêndio.");
                return StatusCode(StatusCodes.Status400BadRequest, "Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar relatório de incêndio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar relatório de incêndio: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    await _relatorioIncendioService.DeleteAsync(id);
                    _logService.LogInformation("Relatório de incêndio deletado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Solicitação inválida para deletar relatório de incêndio.");
                return StatusCode(StatusCodes.Status400BadRequest, "Solicitação não foi possível de ser executada");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao deletar relatório de incêndio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar relatório de incêndio: {ex.Message}");
            }
        }
    }
}
