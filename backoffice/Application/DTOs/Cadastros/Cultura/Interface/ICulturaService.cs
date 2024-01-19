using Application.DTOs.Cadastros.Cultura.ViewModel;

namespace Application.DTOs.Cadastros.Cultura.Interface;

public interface ICulturaService 
{
    Task<IEnumerable<CulturaViewModel>> GetAllAsync();

    Task<CulturaViewModel> GetByIdAsync(int id);

    Task AddAsync(CulturaViewModel obj);

    Task UpdateAsync(CulturaViewModel obj);

    Task DeleteAsync(int id);
}
