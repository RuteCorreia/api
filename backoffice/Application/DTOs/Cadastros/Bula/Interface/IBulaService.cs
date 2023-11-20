using Application.DTOs.Cadastros.Bula.ViewModel;

namespace Application.DTOs.Cadastros.Bula.Interface;

public interface IBulaService 
{
    Task<IEnumerable<BulaViewModel>> GetAllAsync();

    Task<BulaViewModel> GetByIdAsync(int id);

    Task AddAsync(BulaViewModel obj);

    Task UpdateAsync(BulaViewModel obj);

    Task DeleteAsync(int id);
}
