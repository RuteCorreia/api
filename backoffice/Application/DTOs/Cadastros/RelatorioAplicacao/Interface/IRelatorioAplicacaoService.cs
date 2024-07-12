using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

public interface IRelatorioAplicacaoService
{
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync();
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllByIdEmpresaAsync(string? idEmpresa);
    Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id);
    Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataCriacaoAsync(DateTime Date, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusAsync(string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaAsync(string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataAlteracaoAsync(DateTime Date, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetNovosAsync(DateTime? date, string? idEmpresa);
    Task UpdateAsync(RelatorioAplicacaoViewModel obj);
    Task UpdateIsMapaAsync(List<RelatorioAplicacaoViewModel> obj);
    Task DeleteAsync(int id);
}
