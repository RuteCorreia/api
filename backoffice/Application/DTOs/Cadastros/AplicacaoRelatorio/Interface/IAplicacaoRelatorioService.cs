using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;

public interface IAplicacaoRelatorioService 
{
    Task<IEnumerable<AplicacaoRelatorioViewModel>> GetAllAsync();

    Task<AplicacaoRelatorioViewModel> GetByIdAsync(int id);

    Task<int> AddAsync(AplicacaoRelatorioViewModel obj);

    Task UpdateAsync(AplicacaoRelatorioViewModel obj);

    Task DeleteAsync(int id);
}
