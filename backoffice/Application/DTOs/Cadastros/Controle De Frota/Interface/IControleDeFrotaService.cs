using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;

namespace Application.DTOs.Cadastros.Controle_De_Frota.Interface;

public interface IControleDeFrotaService 
{
    Task<IEnumerable<ControleDeFrotaViewModel>> GetAllAsync(string? idEmpresa);

    Task<ControleDeFrotaViewModel> GetByIdAsync(int? id);

    Task<int> AddAsync(ControleDeFrotaViewModel obj, string? idEmpresa);

    Task<int?> UpdateAsync(ControleDeFrotaViewModel obj);

    Task DeleteAsync(int id);
}
