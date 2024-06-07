using Application.Application.Servicos.Cadastros.IdentificacaoAreaTratada;
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
using Application.DTOs.Cadastros.RelatorioAplicacao.Interface;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Log.Interface;
using Domain.Entidades.Cadastros.Aplicacao;
using Domain.Entidades.Cadastros.ContratoPrestacaoServico;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
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
        public async Task<ActionResult> Add([FromBody] dynamic obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logService.LogInformation("Received object: " + JsonConvert.SerializeObject(obj));

                    var id = obj.GetProperty("id").GetInt32();
                    var contratanteJson = obj.GetProperty("contratante").ToString();
                    var identificacaoAreaTratadaJson = obj.GetProperty("identificacaoAreaTratada").ToString();
                    var caracteristicasProdutoAplicadoJson = obj.GetProperty("caracteristicasProdutoAplicado").ToString();
                    var recomendacoesTecnicasJson = obj.GetProperty("recomendacoesTecnicas").ToString();
                    var relatorioAplicacaoJson = obj.GetProperty("relatorioAplicacao").ToString();
                    var contratoPrestacaoServicoJson = obj.GetProperty("contratoPrestacaoServico").ToString();
                    var dadosResponsavelJson = obj.GetProperty("dadosResponsavel").ToString();
                    var piloto = obj.GetProperty("piloto").GetString();
                    var executor = obj.GetProperty("executor").GetString();
                    var refDocument = obj.GetProperty("refDocument").GetString();
                    var data = obj.GetProperty("data").GetString();
                    var refUsuario = obj.GetProperty("refUsuario").GetString();


                    // Accessing properties dynamically
                    var contratanteDeserializado = JsonConvert.DeserializeObject<ContratanteViewModel>(contratanteJson);
                    var identificacaoAreaTratadaDeserializado = JsonConvert.DeserializeObject<IdentificacaoAreaTratadaViewModel>(identificacaoAreaTratadaJson);
                    var caracteristicasProdutoAplicadoDeserializado = JsonConvert.DeserializeObject<CaracteristicasProdutoAplicadoViewModel>(caracteristicasProdutoAplicadoJson);
                    var recomendacoesTecnicasDeserializado = JsonConvert.DeserializeObject<AplicacaoRecomendacoesTecnicasViewModel>(recomendacoesTecnicasJson);
                    var relatorioAplicacaoDeserializado = JsonConvert.DeserializeObject<RelatorioAplicacaoViewModel>(relatorioAplicacaoJson);
                    var contratoPrestacaoServicoDeserializado = JsonConvert.DeserializeObject<ContratoPrestacaoServicoViewModel>(contratoPrestacaoServicoJson);
                    var dadosResponsavelDeserializado = JsonConvert.DeserializeObject<DadosResponsavelViewModel>(dadosResponsavelJson);

                    // Log some properties
                    _logService.LogInformation($"Id: {id}, Piloto: {piloto}, Executor: {executor}");


                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    if (recomendacoesTecnicasDeserializado != null)
                    {
                        recomendacoesTecnicasDeserializado.IdAlturaVoo = null;
                        recomendacoesTecnicasDeserializado.IdAeronave = null;
                        recomendacoesTecnicasDeserializado.IdTipoDeProduto = null;
                        recomendacoesTecnicasDeserializado.IdAplicacao = null;
                        recomendacoesTecnicasDeserializado.IdEquipamento = null;
                        recomendacoesTecnicasDeserializado.IdVeiculante = null;
                    }

                    var contratoPrestacaoServico = _contratoPrestacaoServicoService.AddAsync(contratoPrestacaoServicoDeserializado, loggedUser.Item3);
                    var contratanteService = _contratanteService.AddAsync(contratanteDeserializado);
                    var identificacaoAreaTratadaServiceService = _identificacaoAreaTratadaServiceService.AddAsync(identificacaoAreaTratadaDeserializado);
                    var aplicacaoRecomendacoesTecnicasService = _aplicacaoRecomendacoesTecnicasService.AddAsync(recomendacoesTecnicasDeserializado);
                    var caracteristicasProdutoAplicadoService = _caracteristicasProdutoAplicadoService.AddAsync(caracteristicasProdutoAplicadoDeserializado, loggedUser.Item3);
                    var dadosResponsavelService = _dadosResponsavelService.AddAsync(dadosResponsavelDeserializado, loggedUser.Item3);

                    relatorioAplicacaoDeserializado.ContratoPrestacaoServicoId = contratoPrestacaoServico.Result;
                    relatorioAplicacaoDeserializado.ContratanteId = contratanteService.Result;
                    relatorioAplicacaoDeserializado.IdentificacaoAreaTratadaId = identificacaoAreaTratadaServiceService.Result;
                    if (recomendacoesTecnicasDeserializado != null)
                    {
                        relatorioAplicacaoDeserializado.RecomendacoesTecnicasId = aplicacaoRecomendacoesTecnicasService.Result;
                    }
                    relatorioAplicacaoDeserializado.CaracteristicasProdutoAplicadoId = caracteristicasProdutoAplicadoService.Result;
                    relatorioAplicacaoDeserializado.DadosResponsavelId = dadosResponsavelService.Result;
                    relatorioAplicacaoDeserializado.Id = 0;
                    relatorioAplicacaoDeserializado.Piloto = piloto;
                    relatorioAplicacaoDeserializado.Executor = executor;
                    relatorioAplicacaoDeserializado.RefDocument = refDocument;
                    relatorioAplicacaoDeserializado.Data = data;
                    relatorioAplicacaoDeserializado.RefUsuario = refUsuario;

                    _relatorioAplicacaoService.AddAsync(relatorioAplicacaoDeserializado);
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
