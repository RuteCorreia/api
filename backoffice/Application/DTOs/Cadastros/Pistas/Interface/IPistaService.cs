using Application.DTOs.Cadastros.Pistas.ViewModel;

namespace Application.DTOs.Cadastros.Pistas.Interface;

public interface IPistaService 
{
    Task<IEnumerable<PistaViewModel>> GetAllAsync(string? idEmpresa);

    Task<PistaViewModel> GetByIdAsync(int id);
    Task<IEnumerable<PistaViewModel>> GetByNameAsync(string name);
    Task<IEnumerable<PistaAppViewModel>> GetAllAppAsync(string? idEmpresa);

    Task<int> AddAsync(PistaViewModel obj);

    Task UpdateAsync(PistaViewModel obj);

    Task DeleteAsync(int id);
}
