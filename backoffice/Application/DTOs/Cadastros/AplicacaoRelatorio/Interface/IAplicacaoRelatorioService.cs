using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;

public interface IAplicacaoRelatorioService 
{
    Task<IEnumerable<AplicacaoRelatorioViewModel>> GetAllAsync();

    Task<AplicacaoRelatorioViewModel> GetByIdAsync(int id);

    Task<int> AddAsync(StringAplicacaoRelatorioViewModel obj, string? idEmpresa);

    Task UpdateAsync(AplicacaoRelatorioViewModel obj);
    Task<AplicacaoRelatorioViewModel> GetForExportExcelAsync(int id);
    Task DeleteAsync(int id);
}
