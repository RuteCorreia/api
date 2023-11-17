using Application.DTOs.Cadastros.Aeronave.ViewModel;

namespace Application.DTOs.Cadastros.Aeronave.Interface;

public interface IAeronaveService 
{
    Task<IEnumerable<AeronaveViewModel>> GetAllAsync();

    Task<AeronaveViewModel> GetByIdAsync(int id);

    Task AddAsync(AeronaveViewModel obj);

    Task UpdateAsync(AeronaveViewModel obj);

    Task DeleteAsync(int id);
}
