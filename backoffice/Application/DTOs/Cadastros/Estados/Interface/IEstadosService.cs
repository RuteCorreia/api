using Application.DTOs.Cadastros.Estados.ViewModel;

namespace Application.DTOs.Cadastros.Estados.Interface;

public interface IEstadosService 
{
    Task<IEnumerable<EstadosViewModel>> GetAllAsync();

    Task<EstadosViewModel> GetByIdAsync(int id);

    Task AddAsync(EstadosViewModel obj);

    Task UpdateAsync(EstadosViewModel obj);

    Task DeleteAsync(int id);
}
