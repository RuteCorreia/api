using Application.DTOs.Cadastros.Dashboard.ViewModel;

namespace Application.DTOs.Cadastros.Dashboard.Interface
{
    public interface IDashboardService
    {
        Task<IEnumerable<DashboardViewModel>> GetAllAsync(DateTime? dataInicio,DateTime? dataFim,string? idEmpresa,string? usuario,string? nomeAeronave,string? nomeContratante);
        Task<IEnumerable<DashboardViewModel>> GetFaturamentoExportAsync(DateTime? dataInicio, DateTime? dataFim, string? idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
        Task<IEnumerable<DashboardViewModel>> GetRendimentoExportAsync(DateTime? dataInicio, DateTime? dataFim, string? idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante);
        Task<IEnumerable<string>> GetUsuariosDropdownAsync(string? idEmpresa);
        Task<IEnumerable<string>> GetClientesDropdownAsync(string? idEmpresa);
        Task<IEnumerable<string>> GetAeronavesDropdownAsync(string? idEmpresa);
    }
}
