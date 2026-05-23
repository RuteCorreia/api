using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Helpers;
using Domain.Interfaces.Cadastros.Contratante;
using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Application.DTOs.ExportExcel.ViewModel;
using Domain.Interfaces.User;
using Domain.Interfaces.Cadastros.DataRelatorio;
using Application.DTOs.Pdf.Interface;
using System.Globalization;
using Domain.Interfaces.Cadastros.IdentificadorAplicacao;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.Interface;
using Application.DTOs.Cadastros.Contratante.Interface;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.Interface;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.ProdutoAplicado.Interface;
using Application.DTOs.Cadastros.ReceituarioAgronomico.Interface;
using Application.DTOs.Cadastros.AuxiliarPista.Interface;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using System.Linq;

namespace Application.Application.Servicos.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacaoService : IRelatorioAplicacaoService
    {
        private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;
        private readonly ICaracteristicasProdutoAplicadoRepository _caracteristicasProdutoAplicadoRepository;
        private readonly IIdentificacaoAreaTratadaRepository _identificacaoAreaTratadaRepository;
        private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
        private readonly IIdentificadorAplicacaoRepository _identificadorAplicacaoRepository;
        private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly IDataRelatorioRepository _dataRelatorioRepository;
        private readonly IContratanteRepository _contratanteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IContratanteService _contratanteService;
        private readonly IIdentificacaoAreaTratadaService _identificacaoAreaTratadaService;
        private readonly ICaracteristicasProdutoAplicadoService _caracteristicasProdutoAplicadoService;
        private readonly IAplicacaoRecomendacoesTecnicasService _aplicacaoRecomendacoesTecnicasService;
        private readonly IAplicacaoRelatorioService _aplicacaoRelatorioService;
        private readonly IAplicacaoRelatorioItemService _aplicacaoRelatorioItemService;
        private readonly IContratoPrestacaoServicoService _contratoPrestacaoServicoService;
        private readonly IDadosResponsavelService _dadosResponsavelService;
        private readonly IProdutoAplicadoService _produtoAplicadoService;
        private readonly IReceituarioAgronomicoService _receituarioAgronomicoService;

        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;

        private readonly IAuxiliarPistaService _auxiliarPistaService;

        public RelatorioAplicacaoService(
            IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository,
            ICaracteristicasProdutoAplicadoRepository caracteristicasProdutoAplicadoRepository,
            IIdentificacaoAreaTratadaRepository identificacaoAreaTratadaRepository,
            IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository,
            IIdentificadorAplicacaoRepository identificadorAplicacaoRepository,
            IAplicacaoRelatorioRepository aplicacaoRelatorioRepository,
            IRelatorioAplicacaoRepository relatorioAplicacaoRepository,
            IDataRelatorioRepository dataRelatorioRepository,
            IContratanteRepository contratanteRepository,
            IUsuarioRepository usuarioRepository,
            IContratanteService contratanteService,
            IIdentificacaoAreaTratadaService identificacaoAreaTratadaService,
            ICaracteristicasProdutoAplicadoService caracteristicasProdutoAplicadoService,
            IAplicacaoRecomendacoesTecnicasService aplicacaoRecomendacoesTecnicasService,
            IAplicacaoRelatorioService aplicacaoRelatorioService,
            IAplicacaoRelatorioItemService aplicacaoRelatorioItemService,
            IContratoPrestacaoServicoService contratoPrestacaoServicoService,
            IDadosResponsavelService dadosResponsavelService,
            IProdutoAplicadoService produtoAplicadoService,
            IReceituarioAgronomicoService receituarioAgronomicoService,
            IPdfService pdfService,
            IMapper mapper,
            IAuxiliarPistaService auxiliarPistaService)
        {
            _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;
            _caracteristicasProdutoAplicadoRepository = caracteristicasProdutoAplicadoRepository;
            _identificacaoAreaTratadaRepository = identificacaoAreaTratadaRepository;
            _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
            _identificadorAplicacaoRepository = identificadorAplicacaoRepository;
            _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _dataRelatorioRepository = dataRelatorioRepository;
            _contratanteRepository = contratanteRepository;
            _usuarioRepository = usuarioRepository;
            _produtoAplicadoService = produtoAplicadoService;
            _receituarioAgronomicoService = receituarioAgronomicoService;
            _contratanteService = contratanteService;
            _identificacaoAreaTratadaService = identificacaoAreaTratadaService;
            _caracteristicasProdutoAplicadoService = caracteristicasProdutoAplicadoService;
            _aplicacaoRecomendacoesTecnicasService = aplicacaoRecomendacoesTecnicasService;
            _aplicacaoRelatorioService = aplicacaoRelatorioService;
            _aplicacaoRelatorioItemService = aplicacaoRelatorioItemService;
            _contratoPrestacaoServicoService = contratoPrestacaoServicoService;
            _dadosResponsavelService = dadosResponsavelService;
            _pdfService = pdfService;
            _mapper = mapper;
            _auxiliarPistaService = auxiliarPistaService;
        }

        public async Task DeleteAsync(int id)
        {
            await _relatorioAplicacaoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync()
        {
            var list = await _relatorioAplicacaoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<ExportRelatorioViewModel> ExportExcelAsync(int? id)
        {
            var ra = await _relatorioAplicacaoRepository.ExportExcelAsync(id);
            var iat = await _identificacaoAreaTratadaRepository.GetForExportExcelAsync(ra.IdentificacaoAreaTratadaId);
            var art = await _aplicacaoRecomendacoesTecnicasRepository.GetForExportExcelAsync(ra.RecomendacoesTecnicasId);
            var cpa = await _caracteristicasProdutoAplicadoRepository.GetForExportExcelAsync(ra.CaracteristicasProdutoAplicadoId);
            var ar = await _aplicacaoRelatorioRepository.GetForExportExcelAsync(ra.AplicacaoRelatorioId);
            var ari = await _aplicacaoRelatorioItemRepository.GetForExportExcelAsync(ar.Id);
            var pro = await _produtoAplicadoService.GetAllByIdRelatorioAplicacaoAsync(id.Value);
            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (var item in ari)
            {
                // Convertendo as strings de horímetro para double
                if (TimeSpan.TryParseExact(item.HoraInicio, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan horaInicial) &&
                    TimeSpan.TryParseExact(item.HoraTermino, @"hh\:mm", CultureInfo.InvariantCulture, out TimeSpan horaFinal))
                {
                    // Calculando a diferença entre os horários
                    TimeSpan duration = horaFinal - horaInicial;

                    // Somando a diferença ao total
                    totalDuration += duration;
                }
            }

            int hours = Math.Abs(totalDuration.Hours);
            int minutes = Math.Abs(totalDuration.Minutes);

            string horasAplicacao = $"{hours}{minutes:D2}";

            string prefixo = "";
            string tipoAeronave = "";
            string[] partesNomeAeronave = art.NomeAeronave.Split('-', StringSplitOptions.TrimEntries);
            if (partesNomeAeronave.Length == 2)
            {
                prefixo = partesNomeAeronave[0].Trim();
                tipoAeronave = partesNomeAeronave[1].Trim();
            }
            else if (partesNomeAeronave.Length == 3)
            {
                prefixo = $"{partesNomeAeronave[0].Trim()} - {partesNomeAeronave[1].Trim()}";
                tipoAeronave = partesNomeAeronave[2].Trim();
            }

            if (tipoAeronave == "AVIAO")
            {
                tipoAeronave = "Convencional";
            }
            else if (tipoAeronave == "DRONE")
            {
                tipoAeronave = "Drone";
            }

            var viewModel = new ExportRelatorioViewModel
            {
                UF = iat.UF,
                Municipio = iat.Cidade,
                TipoAeronave = tipoAeronave,
                PrefixoAeronave = prefixo,
                HorasAplicacao = horasAplicacao,
                Cultura = cpa.Cultura,
                TipoDeServico = cpa.TipoServico,
                ClasseAgrotoxico = cpa.Classe,
                Area = ar.TotalAreaAplicada,
                Agrotoxico = cpa.NomeProduto,
                Adjuvante = cpa.Adjuvante,
                Volume = ar.VolumeAplicacao,
                Dosagem = ar.Dosagem,
                UnidadeDosagem = ar.KG_LT,
                Produtos = pro.ToList()
            };
            return viewModel;
        }
        public async Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id)
        {
            var obj = await _relatorioAplicacaoRepository.GetByIdAsync(id);
            return _mapper.Map<RelatorioAplicacaoViewModel>(obj);
        }

        public async Task UpdateAsync(RelatorioAplicacaoViewModel obj)
        {
            var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            await _relatorioAplicacaoRepository.UpdateAsync(mapProduto);
        }

        public async Task UpdateDataAlteracaoAsync(int? id)
        {
            try
            {
                await _relatorioAplicacaoRepository.UpdateDataAlteracaoAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível atualizar a DataAlteracao para o ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids, int isMapa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByIdsAsync(ids, idEmpresaInt, statusEnvio, isMapa);

            var relatorios = _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);

            foreach (var relatorio in relatorios)
            {
                relatorio.ReceituariosAgronomicos = await _receituarioAgronomicoService.GetAllByIdRelatorioAplicacaoAsync(relatorio.Id);
            }

            return relatorios;
        }

        public async Task UpdateIsMapaAsync(List<int> relatorios, bool condicao)
        {
            foreach (var relatorio in relatorios)
            {
                var relatorioExistente = await _relatorioAplicacaoRepository.GetByIdAsync(relatorio);
                if (relatorioExistente != null)
                {
                    relatorioExistente.IsMapa = condicao;

                    // Obtendo a hora local do Brasil
                    var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                    var dataAlteracao = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brasilTimeZone);

                    relatorioExistente.DataAlteracao = dataAlteracao;

                    var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(relatorioExistente);

                    await _relatorioAplicacaoRepository.UpdateIsMapaAsync(mapProduto);
                }
            }
        }

        public async Task CancelarAsync(int id, string? idEmpresa)
        {
            var relatorioExistente = await _relatorioAplicacaoRepository.GetByIdAsync(id);
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            if (relatorioExistente != null)
            {
                relatorioExistente.StatusEnvio = 4;

                // Obtendo a hora local do Brasil
                var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var dataAlteracao = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brasilTimeZone);

                relatorioExistente.DataAlteracao = dataAlteracao;

                if (relatorioExistente.IdData != null)
                {
                    var base64 = await _dataRelatorioRepository.GetByIdAsync(relatorioExistente.IdData, idEmpresaInt);

                    if (!string.IsNullOrEmpty(base64.Data))
                    {
                        byte[] pdfBytes = Convert.FromBase64String(base64.Data);
                        byte[] pdfComMarcaDagua = await _pdfService.AdicionarMarcaDaguaCanceladoAsync(pdfBytes);
                        string pdfComMarcaDaguaBase64 = Convert.ToBase64String(pdfComMarcaDagua);
                        base64.Data = pdfComMarcaDaguaBase64;
                        await _dataRelatorioRepository.UpdateAsync(base64);
                    }
                }

                var mapProduto = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(relatorioExistente);

                await _relatorioAplicacaoRepository.CancelarAsync(mapProduto);
            }
        }

        public async Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            int identificador;
            if (obj.Id == 0)
            {
                identificador = await _identificadorAplicacaoRepository.AddAsync(idEmpresaInt);
                obj.RefDocument = identificador.ToString();
            }
            else 
            {
                var relatorio = await _relatorioAplicacaoRepository.GetByIdAsync(obj.Id);
                obj.RefDocument = relatorio.RefDocument;
            }
            obj.RefDocument = "320";

            var contratante = await _contratanteRepository.GetByIdAsync(obj.ContratanteId);
            var areaTratada = await _identificacaoAreaTratadaRepository.GetByIdAsync(obj.IdentificacaoAreaTratadaId);
            var mapRelatorio = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            mapRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            var ar = await _aplicacaoRelatorioRepository.GetForExportExcelAsync(mapRelatorio.AplicacaoRelatorioId);

            string dataFormatada = string.Empty;

            if (!string.IsNullOrWhiteSpace(mapRelatorio?.DadosResponsavel?.Data) &&
                long.TryParse(mapRelatorio.DadosResponsavel.Data, out long unixTime))
            {
                var dataConvertida = DateTimeOffset
                    .FromUnixTimeMilliseconds(unixTime)
                    .ToLocalTime();

                dataFormatada = dataConvertida.ToString("dd/MM/yyyy");
            }

            mapRelatorio.NomeRelatorio = $"Aplicação - {mapRelatorio.RefDocument} - {areaTratada.Localizacao} - {contratante.Nome.ToString()} - {dataFormatada} - {ar.TotalAreaAplicada} ha";

            if (obj.Id > 0)
            {
                await _relatorioAplicacaoRepository.UpdateAsync(mapRelatorio);
                var mapRelatorioUpdateReturn = _mapper.Map<RelatorioAplicacaoViewModel>(mapRelatorio);
                return mapRelatorioUpdateReturn;
            }
            else
            {
                var Relatorio = await _relatorioAplicacaoRepository.AddAsync(mapRelatorio);
                var mapRelatorioReturn = _mapper.Map<RelatorioAplicacaoViewModel>(Relatorio);
                return mapRelatorioReturn;
            }
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllByIdEmpresaAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusMapaAsync(idEmpresaInt, statusEnvio);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusMapaMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDateAndIdEmpresaAsync(DateTime Date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataCriacaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataCriacaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataAlteracaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataAlteracaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetNovosAsync(DateTime? offsetDate, string? userId, IEnumerable<string>? roleNames, string? idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var user = await _usuarioRepository.GetByUserIdAsync(userId);
            var relatorios = await _relatorioAplicacaoRepository.GetNovosAsync(offsetDate, user.Nome, roleNames, idEmpresaInt,userId);

            var ralatoriosViewModel = _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(relatorios);

            foreach (var relatorio in ralatoriosViewModel)
            {
                relatorio.Contratante = await _contratanteService.GetByIdAsync(relatorio.ContratanteId!.Value);
                relatorio.IdentificacaoAreaTratada = await _identificacaoAreaTratadaService.GetByIdAsync(relatorio.IdentificacaoAreaTratadaId!.Value);
                relatorio.CaracteristicasProdutoAplicado = await _caracteristicasProdutoAplicadoService.GetByIdAsync(relatorio.CaracteristicasProdutoAplicadoId!.Value, idEmpresa);
                relatorio.AplicacaoRecomendacoesTecnicas = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(relatorio.RecomendacoesTecnicasId!.Value);
                relatorio.AplicacaoRelatorio = await _aplicacaoRelatorioService.GetByIdAsync(relatorio.AplicacaoRelatorioId!.Value);
                relatorio.Aplicacoes = await _aplicacaoRelatorioItemService.GetAllAsync(relatorio.AplicacaoRelatorioId!.Value);
                relatorio.ContratoPrestacaoServico = await _contratoPrestacaoServicoService.GetByIdAsync(relatorio.ContratoPrestacaoServicoId!.Value, idEmpresa);
                relatorio.DadosResponsavel = await _dadosResponsavelService.GetByIdAsync(relatorio.DadosResponsavelId!.Value, idEmpresa);

                // If the logged user has client role ("12"), set the phone from the user into DadosResponsavel.Telefone
                if (roleNames != null && roleNames.Contains("12"))
                {
                    if (relatorio.DadosResponsavel != null)
                    {
                        relatorio.DadosResponsavel.Telefone = user?.Telefone;
                    }
                    else
                    {
                        relatorio.DadosResponsavel = new DadosResponsavelViewModel
                        {
                            Telefone = user?.Telefone
                        };
                    }
                }

                relatorio.ProdutosAplicados = await _produtoAplicadoService.GetAllByIdRelatorioAplicacaoAsync(relatorio.Id);
                relatorio.ReceituariosAgronomicos = await _receituarioAgronomicoService.GetAllByIdRelatorioAplicacaoAsync(relatorio.Id);

                
                if (relatorio.AuxiliarPistaId.HasValue)
                {
                    try
                    {
                        relatorio.AuxiliarPista = await _auxiliarPistaService.GetByIdAsync(relatorio.AuxiliarPistaId.Value);
                    }
                    catch
                    {
                        
                        relatorio.AuxiliarPista = null;
                    }
                }
                else
                {
                    relatorio.AuxiliarPista = null;
                }
            }

            return ralatoriosViewModel;
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByIdsAsync(List<int> ids, string idEmpresa)
        {
            var idEmpresaInt = ConvertTypes.ConvertStringToInt(idEmpresa);
            var relatorios = await _relatorioAplicacaoRepository.GetListByIdsAsync(ids);
            var ralatoriosViewModel = _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(relatorios);

            foreach (var relatorio in ralatoriosViewModel)
            {
                relatorio.Contratante = await _contratanteService.GetByIdAsync(relatorio.ContratanteId!.Value);
                relatorio.IdentificacaoAreaTratada = await _identificacaoAreaTratadaService.GetByIdAsync(relatorio.IdentificacaoAreaTratadaId!.Value);
                relatorio.CaracteristicasProdutoAplicado = await _caracteristicasProdutoAplicadoService.GetByIdAsync(relatorio.CaracteristicasProdutoAplicadoId!.Value, idEmpresa);
                relatorio.AplicacaoRecomendacoesTecnicas = await _aplicacaoRecomendacoesTecnicasService.GetByIdAsync(relatorio.RecomendacoesTecnicasId!.Value);
                relatorio.AplicacaoRelatorio = await _aplicacaoRelatorioService.GetByIdAsync(relatorio.AplicacaoRelatorioId!.Value);
                relatorio.Aplicacoes = await _aplicacaoRelatorioItemService.GetAllAsync(relatorio.AplicacaoRelatorioId!.Value);
                relatorio.ContratoPrestacaoServico = await _contratoPrestacaoServicoService.GetByIdAsync(relatorio.ContratoPrestacaoServicoId!.Value, idEmpresa);
                relatorio.DadosResponsavel = await _dadosResponsavelService.GetByIdAsync(relatorio.DadosResponsavelId!.Value, idEmpresa);
                relatorio.ProdutosAplicados = await _produtoAplicadoService.GetAllByIdRelatorioAplicacaoAsync(relatorio.Id);
                relatorio.ReceituariosAgronomicos = await _receituarioAgronomicoService.GetAllByIdRelatorioAplicacaoAsync(relatorio.Id);

                
                if (relatorio.AuxiliarPistaId.HasValue)
                {
                    try
                    {
                        relatorio.AuxiliarPista = await _auxiliarPistaService.GetByIdAsync(relatorio.AuxiliarPistaId.Value);
                    }
                    catch
                    {
                        relatorio.AuxiliarPista = null;
                    }
                }
                else
                {
                    relatorio.AuxiliarPista = null;
                }
            }

            return ralatoriosViewModel;
        }

    }
}
