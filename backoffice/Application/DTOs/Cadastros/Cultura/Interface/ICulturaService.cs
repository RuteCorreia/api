using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Cultura.ViewModel;

namespace Application.DTOs.Cadastros.Cultura.Interface;

public interface ICulturaService 
{
    Task<IEnumerable<CulturaViewModel>> GetAllAsync(string? idEmpresa);

    Task<CulturaViewModel> GetByIdAsync(int id);

    Task<CulturaViewModel> GetByName(string name, string? idEmpresa);

    Task AddAsync(CulturaViewModel obj, string? idEmpresa);

    Task UpdateAsync(CulturaViewModel obj);

    Task DeleteAsync(int id);
}
