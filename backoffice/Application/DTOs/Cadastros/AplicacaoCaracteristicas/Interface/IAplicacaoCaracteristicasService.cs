using Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoCaracteristicas.Interface;

public interface IAplicacaoCaracteristicasService 
{
    Task<IEnumerable<AplicacaoCaracteristicasViewModel>> GetAllAsync();

    Task<AplicacaoCaracteristicasViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoCaracteristicasViewModel obj);

    Task UpdateAsync(AplicacaoCaracteristicasViewModel obj);

    Task DeleteAsync(int id);
}
