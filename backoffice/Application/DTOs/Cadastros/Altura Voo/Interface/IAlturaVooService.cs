using Application.DTOs.Cadastros.AlturaVoo.ViewModel;

namespace Application.DTOs.Cadastros.AlturaVoo.Interface;

public interface IAlturaVooService 
{
    Task<IEnumerable<AlturaVooViewModel>> GetAllAsync();

    Task<AlturaVooViewModel> GetByIdAsync(int id);

    Task AddAsync(AlturaVooViewModel obj);

    Task UpdateAsync(AlturaVooViewModel obj);

    Task DeleteAsync(int id);
}
