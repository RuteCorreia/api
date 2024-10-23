using AutoMapper;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Helpers;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Contratante;
using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
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

namespace Application.Application.Servicos.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacaoService : IRelatorioAplicacaoService
    {
        private readonly IAplicacaoRecomendacoesTecnicasRepository _aplicacaoRecomendacoesTecnicasRepository;
        private readonly ICaracteristicasProdutoAplicadoRepository _caracteristicasProdutoAplicadoRepository;
        private readonly IIdentificacaoAreaTratadaRepository _identificacaoAreaTratadaRepository;
        private readonly IAplicacaoRelatorioItemRepository _aplicacaoRelatorioItemRepository;
        private readonly IAplicacaoRelatorioRepository _aplicacaoRelatorioRepository;
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly IDataRelatorioRepository _dataRelatorioRepository;
        private readonly IContratanteRepository _contratanteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;

        public RelatorioAplicacaoService(
            IAplicacaoRecomendacoesTecnicasRepository aplicacaoRecomendacoesTecnicasRepository,
            ICaracteristicasProdutoAplicadoRepository caracteristicasProdutoAplicadoRepository,
            IIdentificacaoAreaTratadaRepository identificacaoAreaTratadaRepository,
            IAplicacaoRelatorioItemRepository aplicacaoRelatorioItemRepository,
            IAplicacaoRelatorioRepository aplicacaoRelatorioRepository,
            IRelatorioAplicacaoRepository relatorioAplicacaoRepository,
            IDataRelatorioRepository dataRelatorioRepository,
            IContratanteRepository contratanteRepository,
            IUsuarioRepository usuarioRepository,
            IPdfService pdfService,
            IMapper mapper)
        {
            _aplicacaoRecomendacoesTecnicasRepository = aplicacaoRecomendacoesTecnicasRepository;
            _caracteristicasProdutoAplicadoRepository = caracteristicasProdutoAplicadoRepository;
            _identificacaoAreaTratadaRepository = identificacaoAreaTratadaRepository;
            _aplicacaoRelatorioItemRepository = aplicacaoRelatorioItemRepository;
            _aplicacaoRelatorioRepository = aplicacaoRelatorioRepository;
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _dataRelatorioRepository = dataRelatorioRepository;
            _contratanteRepository = contratanteRepository;
            _usuarioRepository = usuarioRepository;
            _pdfService = pdfService;
            _mapper = mapper;
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
            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (var item in ari)
            {
                // Convertendo as strings de horímetro para double
                if (double.TryParse(item.HorimetroInicial, NumberStyles.Any, CultureInfo.InvariantCulture, out double horimetroInicial) &&
                    double.TryParse(item.HorimetroTermino, NumberStyles.Any, CultureInfo.InvariantCulture, out double horimetroFinal))
                {
                    // Calculando a diferença de horímetro
                    double duration = horimetroFinal - horimetroInicial;

                    // Somando a diferença ao total como um TimeSpan
                    totalDuration += TimeSpan.FromHours(duration); // Adiciona a duração ao total
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
                Volume = art.VolumeAplicacao,
                Dosagem = cpa.DoseProdutoHectare,
                Unidade = cpa.UnidadeDoseProdutoHectare
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
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByIdsAsync(ids, idEmpresaInt, statusEnvio, isMapa);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
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
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
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
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var contratante = await _contratanteRepository.GetByIdAsync(obj.ContratanteId);
            var areaTratada = await _identificacaoAreaTratadaRepository.GetByIdAsync(obj.IdentificacaoAreaTratadaId);
            var mapRelatorio = _mapper.Map<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(obj);
            mapRelatorio.IdEmpresa = idEmpresaInt == 0 ? null : idEmpresaInt;
            var ar = await _aplicacaoRelatorioRepository.GetForExportExcelAsync(mapRelatorio.AplicacaoRelatorioId);
            mapRelatorio.NomeRelatorio = $"Aplicação - {mapRelatorio.RefDocument} - {contratante.Nome.ToString()} - {mapRelatorio.DataCriacao:dd/MM/yyyy} - {areaTratada.Localizacao} - {ar.TotalAreaAplicada}";

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
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusAsync(idEmpresaInt, statusEnvio);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaAsync(string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusMapaAsync(idEmpresaInt, statusEnvio);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByStatusMapaMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var statusEnvio = 0;
            var list = await _relatorioAplicacaoRepository.GetListByMesAsync(idEmpresaInt, statusEnvio, primeiroDiaMes, ultimoDiaMes);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDateAndIdEmpresaAsync(DateTime Date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetAllByIdEmpresaAsync(idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataCriacaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataCriacaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataAlteracaoAsync(DateTime date, string? idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var list = await _relatorioAplicacaoRepository.GetByDataAlteracaoAsync(date, idEmpresaInt);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

        public async Task<IEnumerable<RelatorioAplicacaoViewModel>> GetNovosAsync(DateTime? offsetDate, string? userId)
        {
            var user = await _usuarioRepository.GetByUserIdAsync(userId);
            var list = await _relatorioAplicacaoRepository.GetNovosAsync(offsetDate, user.Nome);
            return _mapper.Map<IEnumerable<RelatorioAplicacaoViewModel>>(list);
        }

    }
}
