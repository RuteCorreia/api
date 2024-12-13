using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;

public interface IAplicacaoRelatorioItemService 
{
    Task<IEnumerable<RelatorioItemViewModel>> GetAllAsync(int aplicacaoRelatorioId);

    Task<RelatorioItemViewModel> GetByIdAsync(int id);

    Task<IEnumerable<RelatorioItemViewModel>> GetAllByAplicacaoRelatorioIdAsync(int aplicacaoRelatorioId);

    Task AddAsync(AplicacaoRelatorioItemViewModel obj);

    //Task UpdateAsync(List<AplicacaoRelatorioItemViewModel> obj);
    Task<IEnumerable<RelatorioItemViewModel>> GetForExportExcelAsync(int id);

    Task DeleteAsync(int id);
}
