using Application.DTOs.Cadastros.Atividade.Interface;
using Application.DTOs.Cadastros.Atividade.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.User;
using Helpers;
using System.Globalization;

namespace Application.Application.Servicos.Cadastros.Atividade
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly ICombateIncendioRepository _combateIncendioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public AtividadeService(
            IRelatorioAplicacaoRepository relatorioAplicacaoRepository,
            ICombateIncendioRepository combateIncendioRepository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _combateIncendioRepository = combateIncendioRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<AtividadeViewModel> GetAtividadeByFiltrosAsync(AtividadeFiltroViewModel atividadeFiltroViewModel, string idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapAtividades = _mapper.Map<Domain.Entidades.Cadastros.Atividade.AtividadeFiltro>(atividadeFiltroViewModel);
            mapAtividades.IdEmpresa = idEmpresaInt;
            
            var atividadesAplicacao = await _relatorioAplicacaoRepository.GetAtividadesByFiltrosAsync(mapAtividades);

            var atividadesIncendio = await _combateIncendioRepository.GetAtividadesByFiltrosAsync(mapAtividades);
            var viewModel = new AtividadeViewModel();

            decimal somaValorTotal = 0m;
            decimal somaExtensoes = 0;
            double somaHorasApliacadas = 0;

            foreach (var atividade in atividadesAplicacao)
            {
                if (TryParseValorTotal(atividade.ValorTotal, out decimal valorTotal))
                {
                    somaValorTotal += valorTotal;
                }

                if (TryParseExtensao(atividade.Extensao, out decimal extensao))
                {
                    somaExtensoes += extensao;
                }
            }

            foreach (var atividade in atividadesIncendio)
            {
                if (atividade.HoraInicial.HasValue && atividade.HorarioFinalOperacao.HasValue)
                {
                    TimeSpan duration = atividade.HorarioFinalOperacao.Value - atividade.HoraInicial.Value;
                    somaHorasApliacadas += duration.TotalHours;
                }

                if (TryParseValorTotal(atividade.ValorTotal, out decimal valorTotal))
                {
                    somaValorTotal += valorTotal;
                }
            }

            viewModel.HorasIncendio = FormatHorasMinutos(somaHorasApliacadas);
            viewModel.ValorTotal = somaValorTotal;
            viewModel.Extensao = somaExtensoes.ToString();

            return viewModel;
        }

        private bool TryParseValorTotal(string valorTotalStr, out decimal valorTotal)
        {
            valorTotal = 0m;

            var cleanString = valorTotalStr
                .Replace("R$", "")
                .Trim()
                .Replace(".", "")
                .Replace(",", ".");

            return decimal.TryParse(cleanString, NumberStyles.Number, CultureInfo.InvariantCulture, out valorTotal);
        }

        private bool TryParseExtensao(string extensaoStr, out decimal extensao)
        {
            extensao = 0m;
            var cleanString = extensaoStr.Trim();
            return decimal.TryParse(cleanString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out extensao);
        }

        private string FormatToReal(decimal value)
        {
            var culture = new CultureInfo("pt-BR");
            return string.Format(culture, "R$ {0:N2}", value);
        }

        private string FormatHorasMinutos(double totalHoras)
        {
            int horas = (int)totalHoras;
            int minutos = (int)((totalHoras - horas) * 60);

            return $"{horas}h {minutos}m";
        }
    }
}
