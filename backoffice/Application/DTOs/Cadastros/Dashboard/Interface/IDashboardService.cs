using Application.DTOs.Cadastros.Dashboard.ViewModel;

namespace Application.DTOs.Cadastros.Dashboard.Interface
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetAllAsync(DateTime? dataInicio,DateTime? dataFim,string? idEmpresa,string? usuario,string? nomeAeronave,string? nomeContratante);
    }
}
