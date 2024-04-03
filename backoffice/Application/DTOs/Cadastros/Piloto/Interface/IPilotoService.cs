using Application.DTOs.Cadastros.Executor.ViewModel;
using Application.DTOs.Cadastros.Piloto.ViewModel;

namespace Application.DTOs.Cadastros.Piloto.Interface;

public interface IPilotoService 
{
    Task<IEnumerable<PilotoViewModel>> GetAllAsync();

    Task<PilotoViewModel> GetByIdAsync(string id);

    Task<PilotoViewModel> GetByLoginAsync(string email, string password);

    Task AddAsync(PilotoViewModel obj);

    Task UpdateAsync(PilotoViewModel obj);

    Task DeleteAsync(int id);
}
