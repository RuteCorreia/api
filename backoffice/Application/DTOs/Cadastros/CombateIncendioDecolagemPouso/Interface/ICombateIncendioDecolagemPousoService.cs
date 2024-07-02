using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;

public interface ICombateIncendioDecolagemPousoService 
{
    Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetAllAsync();

    Task<CombateIncendioDecolagemPousoViewModel> GetByIdAsync(int id);

    Task<int> AddAsync(CombateIncendioDecolagemPousoViewModel obj);

    Task UpdateAsync(CombateIncendioDecolagemPousoViewModel obj);

    Task DeleteAsync(int id);
}
