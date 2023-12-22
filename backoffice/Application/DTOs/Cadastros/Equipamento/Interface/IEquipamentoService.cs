using Application.DTOs.Cadastros.Equipamento.ViewModel;

namespace Application.DTOs.Cadastros.Equipamento.Interface;

public interface IEquipamentoService 
{
    Task<IEnumerable<EquipamentoViewModel>> GetAllAsync();

    Task<EquipamentoViewModel> GetByIdAsync(int id);

    Task AddAsync(EquipamentoViewModel obj);

    Task UpdateAsync(EquipamentoViewModel obj);

    Task DeleteAsync(int id);
}
