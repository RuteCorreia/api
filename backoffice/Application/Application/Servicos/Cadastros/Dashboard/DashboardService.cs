using Application.DTOs.Cadastros.Dashboard.Interface;
using Application.DTOs.Cadastros.Dashboard.ViewModel;
using AutoMapper;
using Domain.Interfaces.Cadastros.Dashboard;
using Helpers;

namespace Application.Application.Servicos.Cadastros.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;   
        }

        public async Task<IEnumerable<DashboardViewModel>> GetAllAsync(
            DateTime? dataInicio,
            DateTime? dataFim,
            string? idEmpresa,
            string? usuario,
            string? nomeAeronave,
            string? nomeContratante)
        {
            var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);

            // Obtém os dados do repositório
            var incendioEntities = await _dashboardRepository.GetAllIncendioAsync(dataInicio, dataFim, idEmpresaInt, usuario, nomeAeronave, nomeContratante);
            var aplicacaoEntities = await _dashboardRepository.GetAllAplicacaoAsync(dataInicio, dataFim, idEmpresaInt, usuario, nomeAeronave, nomeContratante);

            // Mapeia as entidades para ViewModels
            var incendioList = _mapper.Map<List<DashboardViewModel>>(incendioEntities ?? Enumerable.Empty<Domain.Entidades.Cadastros.Dashboard.Dashboard>().ToList());
            var aplicacaoList = _mapper.Map<List<DashboardViewModel>>(aplicacaoEntities ?? Enumerable.Empty<Domain.Entidades.Cadastros.Dashboard.Dashboard>().ToList());

            // Agrupando os dados por ano e mês
            var groupedData = from mes in incendioList.Select(i => new { i.Mes, i.Ano })
                              .Union(aplicacaoList.Select(a => new { a.Mes, a.Ano }))
                              .Distinct()
                              select new DashboardViewModel
                              {
                                  Mes = mes.Mes,
                                  Ano = mes.Ano,
                                  ValorTotal = (incendioList.Where(i => i.Mes == mes.Mes && i.Ano == mes.Ano).Sum(i => i.ValorTotal ?? 0) +
                                                aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ValorTotal ?? 0)),
                                  TotalHoras = (incendioList.Where(i => i.Mes == mes.Mes && i.Ano == mes.Ano).Sum(i => i.TotalHoras ?? 0) +
                                                aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.TotalHoras ?? 0)),
                                  ExtensaoTotal = (aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ExtensaoTotal ?? 0))
                              };

            return groupedData.ToList();
        }
    }
}
