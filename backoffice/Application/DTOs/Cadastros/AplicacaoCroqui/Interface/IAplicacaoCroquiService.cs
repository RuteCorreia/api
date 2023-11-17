using Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoCroqui.Interface;

public interface IAplicacaoCroquiService 
{
    Task<IEnumerable<AplicacaoCroquiViewModel>> GetAllAsync();

    Task<AplicacaoCroquiViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoCroquiViewModel obj);

    Task UpdateAsync(AplicacaoCroquiViewModel obj);

    Task DeleteAsync(int id);
}
