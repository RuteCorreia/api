using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Cultura.ViewModel;

namespace Application.DTOs.Cadastros.Cultura.Interface;

public interface ICulturaService 
{
    Task<IEnumerable<CulturaViewModel>> GetAllAsync();

    Task<CulturaViewModel> GetByIdAsync(int id);

    Task<CulturaViewModel> GetByName(string name);

    Task AddAsync(CulturaViewModel obj);

    Task UpdateAsync(CulturaViewModel obj);

    Task DeleteAsync(int id);
}
