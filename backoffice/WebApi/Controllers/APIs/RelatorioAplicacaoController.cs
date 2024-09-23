using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.DataFormat.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Importação_Planilha.ViewModel;
using Application.DTOs.Log.Interface;
using Domain.Entidades.Cadastros.Aplicacao;
using Domain.Entidades.Cadastros.Cidades;
using Domain.Entidades.Cadastros.Cultura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebApi.HttpRequestInfo;
using Application.DTOs.Cadastros.DataRelatorio.Interface;
using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.RelatorioBase;
using ExcelDataReader;
using System.Data;
using OfficeOpenXml;
using System.IO.Compression;
using System.Linq;

namespace WebApi.Controllers.APIs
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RelatorioAplicacaoController : ControllerBase
    {
        private readonly IAplicacaoRecomendacoesTecnicasService _aplicacaoRecomendacoesTecnicasService;
        private readonly ICaracteristicasProdutoAplicadoService _caracteristicasProdutoAplicadoService;
        private readonly IIdentificacaoAreaTratadaService _identificacaoAreaTratadaService;
        private readonly IContratoPrestacaoServicoService _contratoPrestacaoServicoService;
        private readonly IRelatorioAplicacaoService _relatorioAplicacaoService;
        private readonly IAplicacaoRelatorioService _aplicacaoRelatorioService;
        private readonly IDadosResponsavelService _dadosResponsavelService;
        private readonly LoggedUserInfoService _loggedUserInfoService;
        private readonly IDataRelatorioService _dataRelatorioService;
        private readonly IAuxiliarPistaService _auxiliarPistaService;
        private readonly IContratanteService _contratanteService;
        private readonly IAplicacaoRelatorioItemService _aplicacaoRelatorioItemService;
        private readonly ILogService _logService;


        public RelatorioAplicacaoController(
            IAplicacaoRecomendacoesTecnicasService aplicacaoRecomendacoesTecnicasService,
            ICaracteristicasProdutoAplicadoService caracteristicasProdutoAplicadoService,
            IIdentificacaoAreaTratadaService identificacaoAreaTratadaService,
            IContratoPrestacaoServicoService contratoPrestacaoServicoService,
            IRelatorioAplicacaoService relatorioAplicacaoService,
            IAplicacaoRelatorioService aplicacaoRelatorioService,
            IDadosResponsavelService dadosResponsavelService,
            LoggedUserInfoService loggedUserInfoService,
            IDataRelatorioService dataRelatorioService,
            IAuxiliarPistaService auxiliarPistaService,
            IContratanteService contratanteService,
            IAplicacaoRelatorioItemService aplicacaoRelatorioItemService,
            ILogService logService
            )
        {
            _identificacaoAreaTratadaService = identificacaoAreaTratadaService;
            _aplicacaoRecomendacoesTecnicasService = aplicacaoRecomendacoesTecnicasService;
            _caracteristicasProdutoAplicadoService = caracteristicasProdutoAplicadoService;
            _contratoPrestacaoServicoService = contratoPrestacaoServicoService;
            _relatorioAplicacaoService = relatorioAplicacaoService;
            _aplicacaoRelatorioService = aplicacaoRelatorioService;
            _dadosResponsavelService = dadosResponsavelService;
            _loggedUserInfoService = loggedUserInfoService;
            _dataRelatorioService = dataRelatorioService;
            _auxiliarPistaService = auxiliarPistaService;
            _contratanteService = contratanteService;
            _aplicacaoRelatorioItemService = aplicacaoRelatorioItemService;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetAll(DateTime? date)
        {
            try
            {

                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetNovosAsync(date, loggedUser.Item1);
                _logService.LogInformation("Obter todos os relatórios novos.");
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getAllByEmpresa")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetAllByIdEmpresa()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetAllByIdEmpresaAsync(loggedUser.Item3);
                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getDataFromApp")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetDataFromApp()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetListByStatusAsync(loggedUser.Item3);
                List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
                foreach (var relatorio in relatorios)
                {
                    var receituarioAgronomico = await _caracteristicasProdutoAplicadoService.GetReceituarioAgronomicoAsync(relatorio.CaracteristicasProdutoAplicadoId);

                    var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                    if (!string.IsNullOrEmpty(data.Data))
                    {
                        var relatorioBaseViewModel = new RelatorioBaseViewModel
                        {
                            NomeRelatorio = relatorio.NomeRelatorio,
                            Base64Data = data.Data,
                            IsMapa = relatorio.IsMapa,
                            Id = relatorio.Id,
                            StatusEnvio = relatorio.State,
                            IdCaracteristicasProdutoAplicado = relatorio.CaracteristicasProdutoAplicadoId,
                            ReceituarioAgronomico = receituarioAgronomico,
                        };

                        dataRelatorios.Add(relatorioBaseViewModel);
                    }
                    else
                    {
                        // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                        _logService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                    }
                }

                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(dataRelatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getRelatorioMapa")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetRelatorioMapa()
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetListByStatusMapaAsync(loggedUser.Item3);
                List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
                foreach (var relatorio in relatorios)
                {
                    var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                    if (!string.IsNullOrEmpty(data.Data))
                    {
                        var relatorioBaseViewModel = new RelatorioBaseViewModel
                        {
                            NomeRelatorio = relatorio.NomeRelatorio,
                            Base64Data = data.Data,
                            IsMapa = relatorio.IsMapa,
                            Id = relatorio.Id,
                            DataAlteracao = relatorio.DataAlteracao.HasValue
                                            ? relatorio.DataAlteracao.Value.ToString("dd-MM-yyyy HH:mm:ss")
                                            : null
                        };

                        dataRelatorios.Add(relatorioBaseViewModel);
                    }
                    else
                    {
                        // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                        _logService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                    }
                }

                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(dataRelatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpPut("adicionarReceituarioAgronomico/{id:int}")]
        public async Task<ActionResult> adicionarReceituarioAgronomico(int id, [FromBody] DataFormatViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _caracteristicasProdutoAplicadoService.AdicionarReceituarioAgronomicoAsync(id, obj);
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

        [HttpGet("GetRelatoriosByMes/{mes}/{ano}")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetRelatoriosByMes(int mes, int ano)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

                // Primeiro dia do mês e último dia do mês
                var primeiroDiaMes = new DateTime(ano, mes, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                var relatorios = await _relatorioAplicacaoService.GetListByMesAsync(loggedUser.Item3, primeiroDiaMes, ultimoDiaMes);
                List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
                foreach (var relatorio in relatorios)
                {
                    var receituarioAgronomico = await _caracteristicasProdutoAplicadoService.GetReceituarioAgronomicoAsync(relatorio.CaracteristicasProdutoAplicadoId);

                    var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                    if (!string.IsNullOrEmpty(data.Data))
                    {
                        var relatorioBaseViewModel = new RelatorioBaseViewModel
                        {
                            NomeRelatorio = relatorio.NomeRelatorio,
                            Base64Data = data.Data,
                            IsMapa = relatorio.IsMapa,
                            Id = relatorio.Id,
                            StatusEnvio = relatorio.State,
                            IdCaracteristicasProdutoAplicado = relatorio.CaracteristicasProdutoAplicadoId,
                            ReceituarioAgronomico = receituarioAgronomico,
                        };

                        dataRelatorios.Add(relatorioBaseViewModel);
                    }
                    else
                    {
                        // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                        _logService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                    }
                }

                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(dataRelatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getRelatoriosMapaMes/{mes}/{ano}")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetRelatoriosMapaMes(int mes, int ano)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();

                // Primeiro dia do mês e último dia do mês
                var primeiroDiaMes = new DateTime(ano, mes, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                var relatorios = await _relatorioAplicacaoService.GetListByStatusMapaMesAsync(loggedUser.Item3, primeiroDiaMes, ultimoDiaMes);
                List<RelatorioBaseViewModel> dataRelatorios = new List<RelatorioBaseViewModel>();
                foreach (var relatorio in relatorios)
                {
                    var receituarioAgronomico = await _caracteristicasProdutoAplicadoService.GetReceituarioAgronomicoAsync(relatorio.CaracteristicasProdutoAplicadoId);

                    var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);

                    if (!string.IsNullOrEmpty(data.Data))
                    {
                        var relatorioBaseViewModel = new RelatorioBaseViewModel
                        {
                            NomeRelatorio = relatorio.NomeRelatorio,
                            Base64Data = data.Data,
                            IsMapa = relatorio.IsMapa,
                            Id = relatorio.Id,
                            ReceituarioAgronomico = receituarioAgronomico,
                        };

                        dataRelatorios.Add(relatorioBaseViewModel);
                    }
                    else
                    {
                        // Caso não haja base64 válido, você pode continuar com o próximo relatório ou registrar um aviso
                        _logService.LogWarning($"O relatório com IdData {relatorio.IdData} não possui dados válidos.");
                    }
                }

                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(dataRelatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getByDataCriacao")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetByDataCriacao(DateTime date)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetByDataCriacaoAsync(date, loggedUser.Item3);
                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("getByDataAlteracao")]
        public async Task<ActionResult<IEnumerable<RelatorioAplicacaoViewModel>>> GetByDataAlteracao(DateTime date)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var relatorios = await _relatorioAplicacaoService.GetByDataAlteracaoAsync(date, loggedUser.Item3);
                _logService.LogInformation("Todos os relatórios de aplicação foram recuperados com sucesso.");
                return Ok(relatorios);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar todos os relatórios de aplicação: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult> Add([FromBody] dynamic obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //_logService.LogInformation("Received object: " + JsonConvert.SerializeObject(obj));

                    var piloto = obj.GetProperty("piloto").ToString();
                    var executor = obj.GetProperty("executor").ToString();
                    var refDocument = obj.GetProperty("refDocument").ToString();
                    var auxiliarPistaJson = obj.GetProperty("auxiliarPista").ToString(); // Novo campo auxiliarPistaId **
                                                                                         //   var data = obj.GetProperty("data").ToString();
                    var IdData = obj.GetProperty("idData").GetInt32();
                    var statusEnvio = obj.GetProperty("state").GetInt32(); // Campo a ser implementado **

                    var Id = obj.GetProperty("id").GetInt32();

                    var isDrone = obj.GetProperty("isDrone").GetBoolean(); // Novo campo isDrone 
                    var contratanteJson = obj.GetProperty("contratante").ToString();
                    var identificacaoAreaTratadaJson = obj.GetProperty("identificacaoAreaTratada").ToString();

                    var caracteristicasProdutoAplicadoJson = obj.GetProperty("caracteristicasProdutoAplicado").ToString();
                    var recomendacoesTecnicasJson = obj.GetProperty("recomendacoesTecnicas").ToString();
                    var relatorioAplicacaoJson = obj.GetProperty("relatorioAplicacao").ToString(); // Aplicaçoes(aplicacaorelatorioitem)
                    var contratoPrestacaoServicoJson = obj.GetProperty("contratoPrestacaoServico").ToString();
                    var dadosResponsavelJson = obj.GetProperty("dadosResponsavel").ToString();
                    var refUsuario = obj.GetProperty("refUsuario").ToString();
                    //var pilotoId = obj.GetProperty("pilotoId").GetInt32(); // Novo campo pilotoId **
                    //var executorId = obj.GetProperty("executorId").GetInt32(); // Novo campo executorId **
                    var dataCriacao = obj.GetProperty("dataCriacao").GetDateTime(); // Novo campo dataCriacao **
                    var dataAlteracao = obj.GetProperty("dataAlteracao").GetDateTime(); // Novo campo dataAlteracao **
                    //var culturaId = obj.GetProperty("culturaId").GetInt32(); // Novo campo culturaId **


                    var contratanteViewModel = JsonConvert.DeserializeObject<ContratanteViewModel>(contratanteJson);

                    var identificacaoAreaTratadaViewModel = JsonConvert.DeserializeObject<IdentificacaoAreaTratadaViewModel>(identificacaoAreaTratadaJson);
                    string croquiAreaString = JsonConvert.SerializeObject(identificacaoAreaTratadaViewModel.CroquiArea);
                    var areaTratadaViewModel = new AreaTratadaViewModel()
                    {
                        Id = identificacaoAreaTratadaViewModel.Id,
                        UF = identificacaoAreaTratadaViewModel.UF,
                        Cidade = identificacaoAreaTratadaViewModel.Cidade,
                        Localizacao = identificacaoAreaTratadaViewModel.Localizacao,
                        Cultura = identificacaoAreaTratadaViewModel.Cultura,
                        Extensao = identificacaoAreaTratadaViewModel.Extensao,
                        CroquiArea = croquiAreaString,
                        Gravacao = identificacaoAreaTratadaViewModel.Gravacao,
                        Marcadores = identificacaoAreaTratadaViewModel.Marcadores
                    };


                    var caracteristicasProdutoAplicadoViewModel = JsonConvert.DeserializeObject<CaracteristicasProdutoAplicadoViewModel>(caracteristicasProdutoAplicadoJson);
                    string receituarioAgronomicoString = JsonConvert.SerializeObject(caracteristicasProdutoAplicadoViewModel.ReceiturarioAgronomico);
                    var receituarioAgronomicoViewModel = new ProdutoAplicadoViewModel()
                    {
                        Id = caracteristicasProdutoAplicadoViewModel.Id,
                        Cultura = caracteristicasProdutoAplicadoViewModel.Cultura,
                        ReceiturarioAgronomico = receituarioAgronomicoString,
                        NomeProduto = caracteristicasProdutoAplicadoViewModel.NomeProduto,
                        ClassificacaoToxicologica = caracteristicasProdutoAplicadoViewModel.ClassificacaoToxicologica,
                        Classe = caracteristicasProdutoAplicadoViewModel.Classe,
                        TipoFormulacao = caracteristicasProdutoAplicadoViewModel.TipoFormulacao,
                        AlvoBiologico = caracteristicasProdutoAplicadoViewModel.AlvoBiologico,
                        DoseProdutoHectare = caracteristicasProdutoAplicadoViewModel.DoseProdutoHectare,
                        UnidadeDoseProdutoHectare = caracteristicasProdutoAplicadoViewModel.UnidadeDoseProdutoHectare,
                        Adjuvante = caracteristicasProdutoAplicadoViewModel.Adjuvante,
                        TipoServico = caracteristicasProdutoAplicadoViewModel.TipoServico,
                        NumeroReceituarioAgronomico = caracteristicasProdutoAplicadoViewModel.NumeroReceituarioAgronomico,
                        DataEmissao = caracteristicasProdutoAplicadoViewModel.DataEmissao,
                        IsReceituarioImage = caracteristicasProdutoAplicadoViewModel.IsReceituarioImage,
                    };

                    var aplicacaoRecomendacoesTecnicasViewModel = JsonConvert.DeserializeObject<AplicacaoRecomendacoesTecnicasViewModel>(recomendacoesTecnicasJson);

                    var aplicacaoRelatorio = JsonConvert.DeserializeObject<AplicacaoRelatorioViewModel>(relatorioAplicacaoJson);
                    var aplicacoesViewModel = aplicacaoRelatorio.Aplicacoes;
                    var listaAplicacaoRelatorioItem = new List<RelatorioItemViewModel>();
                    for (int i = 0; i < aplicacoesViewModel.Count; i++)
                    {
                        var item = aplicacoesViewModel[i];
                        string imagemCondicaoClimatica = JsonConvert.SerializeObject(aplicacaoRelatorio.Aplicacoes[i].ImagemCondicaoClimatica);


                        var relatorioItemViewModel = new RelatorioItemViewModel()
                        {
                            Id = item.Id,
                            IdAplicacaoRelatorio = item.IdAplicacaoRelatorio,
                            HoraInicio = item.HoraInicio,
                            HorimetroInicial = item.HorimetroInicial,
                            HoraFinal = item.HoraFinal,
                            HorimetroFinal = item.HorimetroFinal,
                            TemperaturaInicial = item.TemperaturaInicial,
                            TemperaturaFinal = item.TemperaturaFinal,
                            UmidadeRelativaArInicial = item.UmidadeRelativaArInicial,
                            UmidadeRelativaArFinal = item.UmidadeRelativaArFinal,
                            VentoInicial = item.VentoInicial,
                            VentoFinal = item.VentoFinal,
                            ImagemCondicaoClimatica = imagemCondicaoClimatica,
                            DataAplicacao = item.DataAplicacao
                        };

                        // Adicione o relatorioItemViewModel a uma coleção ou processe conforme necessário
                        listaAplicacaoRelatorioItem.Add(relatorioItemViewModel);
                    }

                    var aplicacaoRelatorioViewModel = new StringAplicacaoRelatorioViewModel()
                    {
                        Id = aplicacaoRelatorio.Id,
                        Dosagem = aplicacaoRelatorio.Dosagem,
                        UnidadeDosagem = aplicacaoRelatorio.UnidadeDosagem,
                        VolumeAplicacao = aplicacaoRelatorio.VolumeAplicacao,
                        TotalAreaAplicada = aplicacaoRelatorio.TotalAreaAplicada,
                        Observacoes = aplicacaoRelatorio.Observacoes,
                        Cultura = aplicacaoRelatorio.Cultura,
                        ProdutoAplicado = aplicacaoRelatorio.ProdutoAplicado,
                        LocalizacaoPistaCodigoICAO = aplicacaoRelatorio.LocalizacaoPistaCodigoICAO,
                        Lat = aplicacaoRelatorio.Lat,
                        Long = aplicacaoRelatorio.Long,
                        Densidade = aplicacaoRelatorio.Densidade,
                        RelatorioDGPS = aplicacaoRelatorio.RelatorioDGPS,
                        UnidadeVolumeAplicacao = aplicacaoRelatorio.UnidadeVolumeAplicacao,
                        MapaAplicado = aplicacaoRelatorio.MapaAplicado,
                        Aplicacoes = listaAplicacaoRelatorioItem
                    };

                    var contratoPrestacaoServicoViewModel = JsonConvert.DeserializeObject<ContratoPrestacaoServicoViewModel>(contratoPrestacaoServicoJson);
                    var dadosResponsavelViewModel = JsonConvert.DeserializeObject<DadosResponsavelViewModel>(dadosResponsavelJson);
                    var auxiliarPistaViewModel = JsonConvert.DeserializeObject<AuxiliarPistaViewModel>(auxiliarPistaJson);

                    var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                    //if (aplicacaoRecomendacoesTecnicasViewModel != null)
                    //{
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdAlturaVoo = null;
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdAeronave = null;
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdTipoDeProduto = null;
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdAplicacao = null;
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdEquipamento = null;
                    //    aplicacaoRecomendacoesTecnicasViewModel.IdVeiculante = null;
                    //}

                    var IdcaracteristicasProdutoAplicado = await _caracteristicasProdutoAplicadoService.AddAsync(receituarioAgronomicoViewModel, loggedUser.Item3);
                    var IdAuxiliarPista = await _auxiliarPistaService.AddAsync(auxiliarPistaViewModel, loggedUser.Item3); // OK
                    if (IdAuxiliarPista == 0)
                    {
                        IdAuxiliarPista = null;
                    }
                    var Idcontratante = await _contratanteService.AddAsync(contratanteViewModel, loggedUser.Item3); // OK
                    var IdIdentificacaoAreaTratada = await _identificacaoAreaTratadaService.AddAsync(areaTratadaViewModel, loggedUser.Item3); // OK
                    var IdrecomendacoesTecnicas = aplicacaoRecomendacoesTecnicasViewModel != null ? await _aplicacaoRecomendacoesTecnicasService.AddAsync(aplicacaoRecomendacoesTecnicasViewModel, loggedUser.Item3) : (int?)null; // OK
                    var IdAplicacaoRelatorio = await _aplicacaoRelatorioService.AddAsync(aplicacaoRelatorioViewModel, loggedUser.Item3); // OK
                    var IdcontratoPrestacaoServico = await _contratoPrestacaoServicoService.AddAsync(contratoPrestacaoServicoViewModel, loggedUser.Item3);
                    var IdDadosResponsavel = await _dadosResponsavelService.AddAsync(dadosResponsavelViewModel, loggedUser.Item3);

                    var relatorioAplicacaoViewModel = new RelatorioAplicacaoViewModel();
                    relatorioAplicacaoViewModel.Piloto = piloto;
                    relatorioAplicacaoViewModel.Executor = executor;
                    relatorioAplicacaoViewModel.Id = Id;
                    relatorioAplicacaoViewModel.RefDocument = refDocument;
                    relatorioAplicacaoViewModel.AuxiliarPistaId = IdAuxiliarPista;
                    // relatorioAplicacaoViewModel.Data = data;
                    relatorioAplicacaoViewModel.Data = "";
                    relatorioAplicacaoViewModel.IsDrone = isDrone;
                    relatorioAplicacaoViewModel.AuxiliarPistaId = IdAuxiliarPista;
                    relatorioAplicacaoViewModel.ContratanteId = Idcontratante;
                    relatorioAplicacaoViewModel.IdentificacaoAreaTratadaId = IdIdentificacaoAreaTratada;
                    relatorioAplicacaoViewModel.CaracteristicasProdutoAplicadoId = IdcaracteristicasProdutoAplicado;
                    relatorioAplicacaoViewModel.RecomendacoesTecnicasId = IdrecomendacoesTecnicas;
                    relatorioAplicacaoViewModel.AplicacaoRelatorioId = IdAplicacaoRelatorio;
                    relatorioAplicacaoViewModel.ContratoPrestacaoServicoId = IdcontratoPrestacaoServico;
                    relatorioAplicacaoViewModel.DadosResponsavelId = IdDadosResponsavel;
                    relatorioAplicacaoViewModel.RefUsuario = refUsuario;
                    //relatorioAplicacaoViewModel.CulturaId = culturaId;
                    //relatorioAplicacaoViewModel.PilotoId = pilotoId;
                    //relatorioAplicacaoViewModel.ExecutorId = executorId;
                    relatorioAplicacaoViewModel.DataCriacao = dataCriacao;
                    relatorioAplicacaoViewModel.DataAlteracao = dataAlteracao;
                    relatorioAplicacaoViewModel.State = statusEnvio;
                    relatorioAplicacaoViewModel.IdData = IdData;


                    var relatorioAplicacao = await _relatorioAplicacaoService.AddAsync(relatorioAplicacaoViewModel, loggedUser.Item3); // OK

                    var aplicacoes = await _aplicacaoRelatorioItemService.GetAllByAplicacaoRelatorioIdAsync(IdAplicacaoRelatorio);

                    //_logService.LogInformation("Novo relatório de aplicação adicionado com sucesso.");

                    var result = new
                    {
                        id = relatorioAplicacao.Id,
                        contratanteId = Idcontratante,
                        identificacaoAreaTratadaId = IdIdentificacaoAreaTratada,
                        caracteristicasProdutoAplicadoId = IdcaracteristicasProdutoAplicado,
                        recomendacoesTecnicasId = IdrecomendacoesTecnicas,
                        contratoPrestacaoServicoId = IdcontratoPrestacaoServico,
                        dadosResponsavelId = IdDadosResponsavel,
                        auxiliarPistaId = IdAuxiliarPista,
                        idData = IdData,
                        relatorioAplicacao = new
                        {
                            id = IdAplicacaoRelatorio,
                            aplicacoes = aplicacoes.Select(item => item.Id).ToArray()
                        },
                    };

                    //return Ok(relatorioAplicacaoId);
                    var jsonResult = JsonConvert.SerializeObject(result);
                    return Ok(result);
                }

                //_logService.LogWarning("Modelo inválido ao adicionar novo relatório de aplicação.");
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

        [HttpPut("updateIsmapa/{condicao}")]
        public async Task<ActionResult> UpdateIsMapa([FromBody] List<int> obj, bool condicao)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    await _relatorioAplicacaoService.UpdateIsMapaAsync(obj, condicao);
                    _logService.LogInformation("Campo IsMapa do relatório de aplicação atualizado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao atualizar campo IsMapa do relatório de aplicação.");
                return BadRequest("Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao atualizar campo IsMapa do relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar campo IsMapa do relatório de aplicação: {ex.Message}");
            }
        }

        [HttpPut("Cancelar/{id}")]
        public async Task<ActionResult> Cancelar(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _relatorioAplicacaoService.CancelarAsync(id);
                    _logService.LogInformation("relatório de aplicação cancelado com sucesso.");
                    return Ok();
                }

                _logService.LogWarning("Modelo inválido ao cancelar relatório de aplicação.");
                return BadRequest("Modelo inválido");
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao cancelar relatório de aplicação: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cancelar relatório de aplicação: {ex.Message}");
            }
        }


        [HttpPost("DownloadRelatoriosMes")]
        public async Task<IActionResult> DownloadRelatoriosMes([FromBody] DownloadRelatorioRequest request)
        {
            try
            {
                var loggedUser = _loggedUserInfoService.GetLoggedUserIdentityIdAndRole();
                var isMapa = 0;
                var relatorioIds = request.Ids.Select(x => x.Id).ToList();
                var relatorios = await _relatorioAplicacaoService.GetListByIdsAsync(loggedUser.Item3, relatorioIds, isMapa);

                using (var memoryStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var relatorio in relatorios)
                        {
                            var relatorioRequest = request.Ids.FirstOrDefault(x => x.Id == relatorio.Id);

                            // Nome da pasta com base no NomeRelatorio
                            string folderName = relatorio.NomeRelatorio.Replace("/", "-").Replace("\\", "-");

                            // Adicionar o arquivo do relatório
                            var data = await _dataRelatorioService.GetByIdAsync(relatorio.IdData, loggedUser.Item3);
                            if (!string.IsNullOrEmpty(data.Data))
                            {
                                var relatorioBytes = Convert.FromBase64String(data.Data);
                                var nomeArquivoRelatorio = $"{folderName}/{folderName}.pdf"; // Nome do arquivo igual ao da pasta
                                var entry = archive.CreateEntry(nomeArquivoRelatorio, System.IO.Compression.CompressionLevel.Fastest);

                                using (var entryStream = entry.Open())
                                {
                                    entryStream.Write(relatorioBytes, 0, relatorioBytes.Length);
                                }
                            }

                            // Adicionar o arquivo do ReceituarioAgronomico, se disponível
                            if (relatorioRequest?.ReceituarioAgronomico != null &&
                                !string.IsNullOrEmpty(relatorioRequest.ReceituarioAgronomico.Data))
                            {
                                var receituarioBytes = Convert.FromBase64String(relatorioRequest.ReceituarioAgronomico.Data);
                                string receituarioFileName = $"{folderName}/receituarioAgronomico"; // Nome do arquivo do receituário

                                // Verificar o formato do ReceituarioAgronomico
                                if (relatorioRequest.ReceituarioAgronomico.Format.ToLower() == "pdf")
                                {
                                    receituarioFileName += ".pdf";
                                }
                                else if (relatorioRequest.ReceituarioAgronomico.Format.ToLower() == "png")
                                {
                                    receituarioFileName += ".png";
                                }
                                else
                                {
                                    _logService.LogWarning($"Formato desconhecido: {relatorioRequest.ReceituarioAgronomico.Format}");
                                    continue;
                                }

                                var receituarioEntry = archive.CreateEntry(receituarioFileName, System.IO.Compression.CompressionLevel.Fastest);

                                using (var entryStream = receituarioEntry.Open())
                                {
                                    entryStream.Write(receituarioBytes, 0, receituarioBytes.Length);
                                }
                            }
                        }
                    }

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return File(memoryStream.ToArray(), "application/zip", $"relatorios-aplicacao-{request.Mes}-{request.Ano}.zip");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, $"Erro ao recuperar e compactar os relatórios: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar e compactar os relatórios: {ex.Message}");
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
