using Application.DTOs.Cadastros.Atividade.Interface;
using Application.DTOs.Cadastros.Atividade.ViewModel;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.User;
using System.Globalization;

namespace Application.Application.Servicos.Cadastros.Atividade
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly ICombateIncendioRepository _combateIncendioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public AtividadeService(
            IRelatorioAplicacaoRepository relatorioAplicacaoRepository,
            ICombateIncendioRepository combateIncendioRepository,
            IUsuarioRepository usuarioRepository)
        {
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _combateIncendioRepository = combateIncendioRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<AtividadeViewModel> GetAtividadeByContratanteAsync(string contratante)
        {
            var atividadesAplicacao = await _relatorioAplicacaoRepository.GetAtividadeByContratanteAsync(contratante);
            var atividadesIncendio = await _combateIncendioRepository.GetAtividadeByContratanteAsync(contratante);
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
            viewModel.ValorTotal = FormatToReal(somaValorTotal);
            viewModel.Extensao = somaExtensoes.ToString();

            return viewModel;
        }

        public async Task<AtividadeViewModel> GetAtividadeByExecutorAsync(string executor)
        {
            var atividadesAplicacao = await _relatorioAplicacaoRepository.GetAtividadeByExecutorAsync(executor);
            var executorEntity  = await 
            var atividadesIncendio = await _combateIncendioRepository.GetAtividadeByExecutorAsync(executor);
            var viewModel = new AtividadeViewModel();

            decimal somaValorTotal = 0m;
            decimal somaExtensoes = 0;

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

            viewModel.ValorTotal = FormatToReal(somaValorTotal);
            viewModel.Extensao = somaExtensoes.ToString();

            return viewModel;
        }

        public async Task<AtividadeViewModel> GetAtividadeByPilotoAsync(string piloto)
        {
            var atividades = await _relatorioAplicacaoRepository.GetAtividadeByPilotoAsync(piloto);
            var viewModel = new AtividadeViewModel();

            decimal somaValorTotal = 0m;
            decimal somaExtensoes = 0;

            foreach (var atividade in atividades)
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

            viewModel.ValorTotal = FormatToReal(somaValorTotal);
            viewModel.Extensao = somaExtensoes.ToString();

            return viewModel;
        }

        public async Task<AtividadeViewModel> GetAtividadeByPrefixoAsync(string prefixoAeronave)
        {
            var atividades = await _relatorioAplicacaoRepository.GetAtividadeByPrefixoAsync(prefixoAeronave);
            var viewModel = new AtividadeViewModel();

            decimal somaValorTotal = 0m;
            decimal somaExtensoes = 0;

            foreach (var atividade in atividades)
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

            viewModel.ValorTotal = FormatToReal(somaValorTotal);
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
