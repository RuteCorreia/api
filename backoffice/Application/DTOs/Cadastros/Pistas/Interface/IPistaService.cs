using Application.DTOs.Cadastros.Pistas.ViewModel;

namespace Application.DTOs.Cadastros.Pistas.Interface;

public interface IPistaService 
{
    Task<IEnumerable<PistaViewModel>> GetAllAsync();

    Task<PistaViewModel> GetByIdAsync(int id);

    Task<int> AddAsync(PistaViewModel obj);

    Task UpdateAsync(PistaViewModel obj);

    Task DeleteAsync(int id);
}
