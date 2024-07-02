using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;

public interface ICombateIncendioDecolagemPousoService 
{
    Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetAllAsync(string? idEmpresa);

    Task<IEnumerable<CombateIncendioDecolagemPousoViewModel>> GetByCombateIncendioIdAsync(int? combateIncendioId);

    Task<CombateIncendioDecolagemPousoViewModel> GetByIdAsync(int id);

    Task<int> AddAsync(CombateIncendioDecolagemPousoViewModel obj, string? idEmpresa);

    Task UpdateAsync(CombateIncendioDecolagemPousoViewModel obj);

    Task DeleteAsync(int id);
}
