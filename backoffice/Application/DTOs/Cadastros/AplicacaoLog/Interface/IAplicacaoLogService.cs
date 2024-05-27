using Application.DTOs.Cadastros.AplicacaoLog.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoLog.Interface;

public interface IAplicacaoLogService 
{
    Task<IEnumerable<AplicacaoLogViewModel>> GetAllAsync();

    Task<AplicacaoLogViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoLogViewModel obj);

    Task UpdateAsync(AplicacaoLogViewModel obj);

    Task DeleteAsync(int id);
}
