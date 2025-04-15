using Application.DTOs.Cadastros.Aeronave.ViewModel;

namespace Application.DTOs.Cadastros.Aeronave.Interface;

public interface IAeronaveService 
{
    Task<IEnumerable<AeronaveViewModel>> GetAllAsync(string? idEmpresa);

    Task<AeronaveViewModel> GetByIdAsync(int id, string? idEmpresa);

    Task<(bool,string)> AddAsync(AeronaveViewModel obj, string? idEmpresa);

    Task UpdateAsync(AeronaveViewModel obj);

    Task<IEnumerable<AeronaveViewModel>> GetByNameAsync(string name, string? idEmpresa);

    Task DeleteAsync(int id, string? idEmpresa);
}
