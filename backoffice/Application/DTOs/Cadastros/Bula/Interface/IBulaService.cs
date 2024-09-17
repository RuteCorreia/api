using Application.DTOs.Cadastros.Bula.ViewModel;

namespace Application.DTOs.Cadastros.Bula.Interface;

public interface IBulaService 
{
    Task<IEnumerable<BulaViewModel>> GetAllAsync(string? idEmpresa);

    Task<BulaViewModel> GetByIdAsync(int id);
    Task<BulaViewModel> GetByName(string name, string? idEmpresa);
    Task<IEnumerable<int>> GetDistinctBulaAsync(string? idEmpresa);

    Task AddAsync(BulaViewModel obj, string? idEmpresa);

    Task UpdateAsync(BulaViewModel obj);

    Task DeleteAsync(int id);
}
