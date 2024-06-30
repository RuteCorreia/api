using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;

public interface IAplicacaoRelatorioItemService 
{
    Task<IEnumerable<AplicacaoRelatorioItemViewModel>> GetAllAsync();

    Task<AplicacaoRelatorioItemViewModel> GetByIdAsync(int id);

    Task<IEnumerable<AplicacaoRelatorioItemViewModel>> GetAllByAplicacaoRelatorioIdAsync(int aplicacaoRelatorioId);

    Task AddAsync(AplicacaoRelatorioItemViewModel obj);

    Task UpdateAsync(AplicacaoRelatorioItemViewModel obj);

    Task DeleteAsync(int id);
}
