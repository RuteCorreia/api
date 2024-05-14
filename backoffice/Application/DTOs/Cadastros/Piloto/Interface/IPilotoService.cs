using Application.DTOs.Cadastros.Piloto.ViewModel;

namespace Application.DTOs.Cadastros.Piloto.Interface;

public interface IPilotoService 
{
    Task<IEnumerable<PilotoViewModel>> GetAllAsync(string? idEmpresa);

    Task<PilotoViewModel?> GetByIdAsync(string id, string? idEmpresa);
}
