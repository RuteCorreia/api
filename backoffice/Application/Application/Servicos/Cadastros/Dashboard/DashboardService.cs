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
            var frotaEntities = await _dashboardRepository.GetAllFrotaAsync(dataInicio, dataFim, idEmpresaInt, usuario, nomeAeronave, nomeContratante);

            // Mapeia as entidades para ViewModels
            var incendioList = _mapper.Map<List<DashboardViewModel>>(incendioEntities ?? Enumerable.Empty<Domain.Entidades.Cadastros.Dashboard.Dashboard>().ToList());
            var aplicacaoList = _mapper.Map<List<DashboardViewModel>>(aplicacaoEntities ?? Enumerable.Empty<Domain.Entidades.Cadastros.Dashboard.Dashboard>().ToList());
            var frotaList = _mapper.Map<List<DashboardViewModel>>(frotaEntities ?? Enumerable.Empty<Domain.Entidades.Cadastros.Dashboard.Dashboard>().ToList());

            // Agrupando os dados por ano e mês
            var groupedData = from mes in incendioList.Select(i => new { i.Mes, i.Ano })
                              .Union(aplicacaoList.Select(a => new { a.Mes, a.Ano }))
                              .Union(frotaList.Select(f => new { f.Mes, f.Ano }))
                              .Distinct()
                              select new DashboardViewModel
                              {
                                  Mes = mes.Mes,
                                  Ano = mes.Ano,
                                  ValorTotal = (incendioList.Where(i => i.Mes == mes.Mes && i.Ano == mes.Ano).Sum(i => i.ValorTotal ?? 0) +
                                                aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ValorTotal ?? 0)),
                                  TotalHoras = frotaList.Where(f => f.Mes == mes.Mes && f.Ano == mes.Ano).Sum(f => f.TotalHoras ?? 0),
                                  ExtensaoTotal = (aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ExtensaoTotal ?? 0)),
                                  Rendimento = (aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ExtensaoTotal ?? 0) != 0) ?
                                        (aplicacaoList.Where(a => a.Mes == mes.Mes && a.Ano == mes.Ano).Sum(a => a.ExtensaoTotal ?? 0)) /
                                        (frotaList.Where(f => f.Mes == mes.Mes && f.Ano == mes.Ano).Sum(f => f.TotalHoras ?? 0) != 0 ?
                                        frotaList.Where(f => f.Mes == mes.Mes && f.Ano == mes.Ano).Sum(f => f.TotalHoras ?? 0) : 1) : 0
                              };

            return groupedData.ToList();
        }

        public Task<IEnumerable<string>> GetUsuariosDropdownAsync(string? idEmpresa)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                return _dashboardRepository.GetUsuariosDropdownAsync(idEmpresaInt);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<string>> GetClientesDropdownAsync(string? idEmpresa)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                return _dashboardRepository.GetClientesDropdownAsync(idEmpresaInt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<string>> GetAeronavesDropdownAsync(string? idEmpresa)
        {
            try
            {
                var idEmpresaInt = ConvertIdEmpresaFromStringToInt.GetIdEmpresaAsInt(idEmpresa);
                return _dashboardRepository.GetAeronavesDropdownAsync(idEmpresaInt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
