using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;

namespace Application.DTOs.Cadastros.ContratoPrestacaoServico.Interface;

public interface IContratoPrestacaoServicoService
{
    Task<IEnumerable<ContratoPrestacaoServicoViewModel>> GetAllAsync(string? idEmpresa);

    Task<ContratoPrestacaoServicoViewModel> GetByIdAsync(int id, string? idEmpresa);

    Task AddAsync(ContratoPrestacaoServicoViewModel obj, string? idEmpresa);

    Task UpdateAsync(ContratoPrestacaoServicoViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);
}
