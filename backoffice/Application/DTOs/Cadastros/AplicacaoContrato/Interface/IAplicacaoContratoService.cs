using Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoContrato.Interface;

public interface IAplicacaoContratoService 
{
    Task<IEnumerable<AplicacaoContratoViewModel>> GetAllAsync();

    Task<AplicacaoContratoViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoContratoViewModel obj);

    Task UpdateAsync(AplicacaoContratoViewModel obj);

    Task DeleteAsync(int id);
}
