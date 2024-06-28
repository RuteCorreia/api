using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

public interface IRelatorioAplicacaoService
{
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync();
    Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id);
    Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj);
    Task UpdateAsync(RelatorioAplicacaoViewModel obj);
    Task DeleteAsync(int id);
}
