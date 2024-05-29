using Application.DTOs.Cadastros.Veiculante.ViewModel;

namespace Application.DTOs.Cadastros.Veiculante.Interface;

public interface IVeiculanteService 
{
    Task<IEnumerable<VeiculanteViewModel>> GetAllAsync();

    Task<VeiculanteViewModel> GetByIdAsync(int id);

    Task AddAsync(VeiculanteViewModel obj);

    Task UpdateAsync(VeiculanteViewModel obj);

    Task DeleteAsync(int id);
}
