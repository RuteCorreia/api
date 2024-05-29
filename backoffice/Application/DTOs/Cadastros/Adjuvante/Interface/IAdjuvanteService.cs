using Application.DTOs.Cadastros.Adjuvante.ViewModel;

namespace Application.DTOs.Cadastros.Adjuvante.Interface;

public interface IAdjuvanteService 
{
    Task<IEnumerable<AdjuvanteViewModel>> GetAllAsync();

    Task<AdjuvanteViewModel> GetByIdAsync(int id);

    Task AddAsync(AdjuvanteViewModel obj);

    Task UpdateAsync(AdjuvanteViewModel obj);

    Task DeleteAsync(int id);
}
