using Application.DTOs.Cadastros.Atividade.Interface;
using Application.DTOs.Cadastros.Atividade.ViewModel;
using Application.DTOs.Users.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.Atividade;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.User;
using Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

namespace Application.Application.Servicos.Cadastros.Atividade
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IRelatorioAplicacaoRepository _relatorioAplicacaoRepository;
        private readonly ICombateIncendioRepository _combateIncendioRepository;
        private readonly IControleDeFrotaRepository _controleDeFrotaRepository;
        private readonly IMapper _mapper;
        public AtividadeService(
            IRelatorioAplicacaoRepository relatorioAplicacaoRepository,
            ICombateIncendioRepository combateIncendioRepository,
            IControleDeFrotaRepository controleDeFrotaRepository,
            IMapper mapper)
        {
            _relatorioAplicacaoRepository = relatorioAplicacaoRepository;
            _combateIncendioRepository = combateIncendioRepository;
            _controleDeFrotaRepository = controleDeFrotaRepository;
            _mapper = mapper;
        }
        public async Task<AtividadeViewModel> GetAtividadeByFiltrosAsync(AtividadeFiltroViewModel atividadeFiltroViewModel, string idEmpresa)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
            var mapAtividades = _mapper.Map<AtividadeFiltro>(atividadeFiltroViewModel);
            mapAtividades.IdEmpresa = idEmpresaInt;

            var atividadesAplicacao = await _relatorioAplicacaoRepository.GetAtividadesByFiltrosAsync(mapAtividades);
            var atividadesIncendio = await _combateIncendioRepository.GetAtividadesByFiltrosAsync(mapAtividades);

            IEnumerable<Domain.Entidades.Cadastros.Atividade.Atividade> atividadesFrota = null;
            if (mapAtividades.Contratante.IsNullOrEmpty())
            {
                atividadesFrota = await _controleDeFrotaRepository.GetAtividadesByFiltrosAsync(mapAtividades);
            }

            decimal somaValorTotalAplicacao = 0;
            decimal somaValorTotalIncendio = 0;
            double somaExtensoes = 0;
            double somaHorasAplicacao = 0;
            double somaHorasIncendio = 0;
            double somaHorasTranslado = 0;
            var comissoesPilotoAplicacao = new Dictionary<string, decimal>();
            var comissoesExecutorAplicacao = new Dictionary<string, decimal>();
            var comissoesPilotoIncendio = new Dictionary<string, decimal>();
            var comissoesExecutorIncendio = new Dictionary<string, decimal>();

            var viewModel = new AtividadeViewModel();


            foreach (var atividade in atividadesAplicacao)
            {
                if (TryParseValorTotal(atividade.ValorTotalAplicacao, out decimal valorTotal))
                {
                    somaValorTotalAplicacao += valorTotal;
                }
                else
                {
                    somaValorTotalAplicacao += 0; 
                }

                if (!string.IsNullOrWhiteSpace(atividade.Extensao))
                {
                    // Tenta interpretar o número com vírgula como separador decimal
                    string extensao = atividade.Extensao.Replace(",", ".");

                    if (double.TryParse(extensao, NumberStyles.Any, CultureInfo.InvariantCulture, out double extensaoAplicacao))
                    {
                        somaExtensoes += extensaoAplicacao;
                    }
                    else
                    {
                        // Opcional: Log ou tratamento caso o valor não seja válido
                        Console.WriteLine($"Valor inválido: {atividade.Extensao}");
                    }
                }
                else
                {
                    somaExtensoes += 0;
                }


            }

            foreach (var atividade in atividadesIncendio)
            {

                if (TryParseValorTotal(atividade.ValorTotalIncendio, out decimal valorTotal))
                {
                    somaValorTotalIncendio += valorTotal;
                }
            }

            if (atividadesFrota != null)
            {
                foreach (var atividade in atividadesFrota)
                {
                    if (atividade.TotalHorasAplicacao.HasValue && atividade.TotalHorasAplicacao > 0)
                    {
                        somaHorasAplicacao += atividade.TotalHorasAplicacao ?? 0;
                    }

                    if (atividade.TotalHorasIncendio.HasValue && atividade.TotalHorasIncendio > 0)
                    {
                        somaHorasIncendio += atividade.TotalHorasIncendio ?? 0;
                    }

                    if (atividade.TotalHorasTranslado.HasValue && atividade.TotalHorasTranslado > 0)
                    {
                        somaHorasTranslado += atividade.TotalHorasTranslado ?? 0;
                    }
                }
            }


            viewModel.HorasAplicacao = somaHorasAplicacao;
            viewModel.HorasIncendio = somaHorasIncendio;
            viewModel.HorasTranslado = somaHorasTranslado;
            viewModel.ValorAplicacao = somaValorTotalAplicacao;
            viewModel.ValorIncendio = somaValorTotalIncendio;
            viewModel.Extensao = somaExtensoes;

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
