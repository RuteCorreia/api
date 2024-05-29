using Application.DTOs.Cadastros.PlanoDeContrato.ViewModel;

namespace Application.DTOs.Cadastros.PlanoDeContrato.Interface;

public interface IPlanoDeContratoService 
{
    Task<IEnumerable<PlanoDeContratoViewModel>> GetAllAsync();

    Task<PlanoDeContratoViewModel> GetByIdAsync(int id);

    Task AddAsync(PlanoDeContratoViewModel obj);

    Task UpdateAsync(PlanoDeContratoViewModel obj);

    Task DeleteAsync(int id);
}
