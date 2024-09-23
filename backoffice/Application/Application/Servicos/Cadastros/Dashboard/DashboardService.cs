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

            var rendimentoAplicacao = aplicacao.TotalHoras / aplicacao.ExtensaoTotal;
            var rendimentoFormatado = Math.Round(rendimentoAplicacao ?? 0, 2);

            // Retorna um objeto contendo os valores somados
            var dashboardViewModel = new DashboardViewModel
            {
                ValorTotal = incendio.ValorTotal + aplicacao.ValorTotal,
                TotalHoras = incendio.TotalHoras + aplicacao.TotalHoras,
                ExtensaoTotal = aplicacao.ExtensaoTotal ,
                Rendimento = rendimentoFormatado
            };

            return dashboardViewModel;
        }
    }
}
