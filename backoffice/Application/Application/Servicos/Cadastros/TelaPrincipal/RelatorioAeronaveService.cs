using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;
using Application.DTOs.Cadastros.TelaPrincipal.Interface;
using Application.DTOs.Cadastros.TelaPrincipal.ViewModel;
using AutoMapper;
using Domain.Entidades.Cadastros.TelaPrincipal;
using Domain.Interfaces.Cadastros.TelaPrincipal;
using System.Globalization;

namespace Application.Application.Servicos.Cadastros.TelaPrincipal
{
    public class RelatorioAeronaveService : IRelatorioAeronaveService
    {
        private readonly IRelatorioAeronaveRepository _relatorioAeronaveRepository;
        private readonly IMapper _mapper;
        public RelatorioAeronaveService(
            IRelatorioAeronaveRepository relatorioAeronaveRepository,
            IMapper mapper)
        {
            _relatorioAeronaveRepository = relatorioAeronaveRepository;  
            _mapper = mapper;
        }
        public async Task<IEnumerable<RelatorioAeronaveDetalhadoViewModel>> GetAllAsync()
        {
            try
            {
                // Obtenha todos os relatórios
                var relatoriosAeronave = await _relatorioAeronaveRepository.GetAllAplicacaoAsync();
                var relatoriosIncendio = await _relatorioAeronaveRepository.GetAllIncendioAsync();

                var todosRelatorios = relatoriosAeronave
                    .Concat(relatoriosIncendio)
                    .ToList();

                // Mapeia os relatórios para o ViewModel correspondente
                var viewModelList = _mapper.Map<IEnumerable<RelatorioAeronaveViewModel>>(todosRelatorios);

                var cultureInfo = new CultureInfo("pt-BR");

                // Formata os dados e calcula rendimento/valor de horas voadas
                foreach (var viewModel in viewModelList)
                {
                    if (viewModel.TotalHoras > 0)
                    {
                        viewModel.Rendimento = viewModel.ExtensaoTotal / viewModel.TotalHoras;
                        viewModel.ValorHorasVoadas = viewModel.ValorTotal / viewModel.TotalHoras;
                    }
                    else
                    {
                        viewModel.Rendimento = null;
                        viewModel.ValorHorasVoadas = 0;
                    }

                    viewModel.ExtensaoTotalFormatado = $"{viewModel.ExtensaoTotal:N0} ha";
                    viewModel.ValorTotalFormatado = $"R$ {viewModel.ValorTotal.ToString("N2", cultureInfo)}";
                    viewModel.TotalHorasFormatado = $"{viewModel.TotalHoras.ToString("N2", cultureInfo)} horas";
                    viewModel.RendimentoFormatado = viewModel.Rendimento.HasValue
                        ? $"{viewModel.Rendimento.Value.ToString("N2", cultureInfo)} ha/hora"
                        : "N/A";
                    viewModel.ValorHorasVoadasFormatado = $"R$ {viewModel.ValorHorasVoadas.ToString("N2", cultureInfo)}";
                }

                // Agrupa os relatórios por aeronave
                var relatoriosAgrupados = viewModelList
                    .GroupBy(r => r.Aeronave)
                    .Select(grupo => new RelatorioAeronaveDetalhadoViewModel
                    {
                        Aeronave = grupo.Key,
                        ExtensaoTotal = grupo.Sum(r => r.ExtensaoTotal),
                        ValorTotal = grupo.Sum(r => r.ValorTotal),
                        TotalHoras = grupo.Sum(r => r.TotalHoras),
                        Rendimento = grupo.Sum(r => r.ExtensaoTotal) / (grupo.Sum(r => r.TotalHoras) > 0 ? grupo.Sum(r => r.TotalHoras) : 1),
                        ValorHorasVoadas = grupo.Sum(r => r.ValorTotal) / (grupo.Sum(r => r.TotalHoras) > 0 ? grupo.Sum(r => r.TotalHoras) : 1),
                        Comissoes = grupo
                            .SelectMany(r => new List<ComissaoViewModel>
                            {
                        new ComissaoViewModel { Nome = r.Piloto, ValorComissao = CalcularComissaoPiloto(r) },
                        new ComissaoViewModel { Nome = r.Executor, ValorComissao = CalcularComissaoExecutor(r) }
                            })
                            .GroupBy(c => c.Nome) // Agrupa comissões pelo nome
                            .Select(g => new ComissaoViewModel
                            {
                                Nome = g.Key,
                                ValorComissao = g.Sum(c => c.ValorComissao),
                                ValorComissaoFormatado = $"R$ {g.Sum(c => c.ValorComissao).ToString("N2", cultureInfo)}"
                            })
                            .ToList(),
                        HorasDisponiveisRevisao = 48 // Valor fixo para o exemplo
                    }).ToList();

                return relatoriosAgrupados;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter relatórios agrupados por aeronave.", ex);
            }
        }


        private decimal CalcularComissaoPiloto(RelatorioAeronaveViewModel relatorio)
        {
            var idEmpresa = 196;
            var comissao = _relatorioAeronaveRepository.GetComissaoAsync(relatorio.Piloto, idEmpresa);

            decimal comissaoDecimal = comissao ?? 0;
            // Lógica para calcular a comissão do piloto
            return relatorio.ValorTotal * (comissaoDecimal / 100); // Exemplo: 10% de comissão
        }

        private decimal CalcularComissaoExecutor(RelatorioAeronaveViewModel relatorio)
        {
            var idEmpresa = 196;
            var comissao = _relatorioAeronaveRepository.GetComissaoAsync(relatorio.Executor, idEmpresa);

            decimal comissaoDecimal = comissao ?? 0;

            // Lógica para calcular a comissão do executor
            return relatorio.ValorTotal * (comissaoDecimal / 100); // Exemplo: 5% de comissão
        }
    }
}
