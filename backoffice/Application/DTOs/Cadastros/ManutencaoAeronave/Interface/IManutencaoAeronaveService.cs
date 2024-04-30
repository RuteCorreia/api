using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.Interface;

public interface IManutencaoAeronaveService
{
    Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync(string? idEmpresa);

    Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id);

    Task AddAsync(ManutencaoAeronaveViewModel obj, string? idEmpresa);

    Task UpdateAsync(ManutencaoAeronaveViewModel obj);

    Task DeleteAsync(int id);
}
