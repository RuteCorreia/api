using Application.DTOs.Cadastros.Combustivel.ViewModel;

namespace Application.DTOs.Cadastros.Combustivel.Interface;

public interface ICombustivelService 
{
    Task<IEnumerable<CombustivelViewModel>> GetAllAsync();

    Task<CombustivelViewModel> GetByIdAsync(int id);

    Task AddAsync(CombustivelViewModel obj);

    Task UpdateAsync(CombustivelViewModel obj);

    Task DeleteAsync(int id);
}
