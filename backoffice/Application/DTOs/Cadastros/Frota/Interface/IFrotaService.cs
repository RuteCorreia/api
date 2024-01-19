using Application.DTOs.Cadastros.Frota.ViewModel;

namespace Application.DTOs.Cadastros.Frota.Interface;

public interface IFrotaService 
{
    Task<IEnumerable<FrotaViewModel>> GetAllAsync();

    Task<FrotaViewModel> GetByIdAsync(int id);

    Task AddAsync(FrotaViewModel obj);

    Task UpdateAsync(FrotaViewModel obj);

    Task DeleteAsync(int id);
}
