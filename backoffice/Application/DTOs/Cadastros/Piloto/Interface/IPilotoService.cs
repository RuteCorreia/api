using Application.DTOs.Cadastros.Piloto.ViewModel;

namespace Application.DTOs.Cadastros.Piloto.Interface;

public interface IPilotoService 
{
    Task<IEnumerable<PilotoViewModel>> GetAllAsync();

    Task<PilotoViewModel?> GetByIdAsync(string id);
}
