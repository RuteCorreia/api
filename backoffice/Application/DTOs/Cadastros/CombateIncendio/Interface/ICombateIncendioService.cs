using Application.DTOs.Cadastros.CombateIncendio.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendio.Interface;

public interface ICombateIncendioService 
{
    Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync();

    Task<CombateIncendioViewModel> GetByIdAsync(int id);

    Task AddAsync(CombateIncendioViewModel obj);

    Task UpdateAsync(CombateIncendioViewModel obj);

    Task DeleteAsync(int id);
}
