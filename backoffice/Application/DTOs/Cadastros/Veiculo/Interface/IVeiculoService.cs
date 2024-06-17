using Application.DTOs.Cadastros.Veiculo.ViewModel;

namespace Application.DTOs.Cadastros.Veiculo.Interface;

public interface IVeiculoService
{
    Task<IEnumerable<VeiculoViewModel>> GetAllAsync(string? idEmpresa);

    Task<VeiculoViewModel?> GetByIdAsync(int id, string? idEmpresa);

    Task AddAsync(VeiculoViewModel obj, string? idEmpresa);

    Task UpdateAsync(VeiculoViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);

    Task<int?> GetKmAtualByIdAsync(int id, string? idEmpresa);

}
