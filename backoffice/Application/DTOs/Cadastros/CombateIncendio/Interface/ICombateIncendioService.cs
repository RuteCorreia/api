using Application.DTOs.Cadastros.CombateIncendio.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendio.Interface;

public interface ICombateIncendioService 
{
    Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync(DateTime? offsetDate, string? idEmpresa);

    Task<CombateIncendioViewModel> GetByIdAsync(int id);
    
    Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa);

    Task<int> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa);

    Task DeleteAsync(int id);
}
