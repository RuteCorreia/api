using Application.DTOs.Cadastros.Dashboard.Interface;
using Application.DTOs.Cadastros.Dashboard.ViewModel;
using Domain.Interfaces.Cadastros.Dashboard;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardViewModel> GetAllAsync(
            DateTime? dataInicio,
            DateTime? dataFim,
            string? idEmpresa,
            string? usuario,
            string? nomeAeronave,
            string? nomeContratante)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);

            var incendio = await _dashboardRepository.GetAllIncendioAsync(dataInicio, dataFim, idEmpresaInt, usuario, nomeAeronave, nomeContratante);

            var aplicacao = await _dashboardRepository.GetAllAplicacaoAsync(dataInicio, dataFim, idEmpresaInt, usuario, nomeAeronave, nomeContratante);

            var incendioValorTotal = incendio?.ValorTotal ?? 0;
            var aplicacaoValorTotal = aplicacao?.ValorTotal ?? 0;
            var incendioTotalHoras = incendio?.TotalHoras ?? 0;
            var aplicacaoTotalHoras = aplicacao?.TotalHoras ?? 0;
            var aplicacaoExtensaoTotal = aplicacao?.ExtensaoTotal ?? 0;

            // Calcula o rendimento da aplicação
            var rendimentoAplicacao = aplicacaoExtensaoTotal > 0 ? aplicacaoTotalHoras / aplicacaoExtensaoTotal : 0;
            var rendimentoFormatado = Math.Round(rendimentoAplicacao, 2);

            // Retorna um objeto contendo os valores somados
            var dashboardViewModel = new DashboardViewModel
            {
                Mes = aplicacao.Mes,
                ValorTotal = incendioValorTotal + aplicacaoValorTotal,
                TotalHoras = incendioTotalHoras + aplicacaoTotalHoras,
                ExtensaoTotal = aplicacaoExtensaoTotal,
                Rendimento = rendimentoFormatado
            };

            return dashboardViewModel;
        }
    }
}
