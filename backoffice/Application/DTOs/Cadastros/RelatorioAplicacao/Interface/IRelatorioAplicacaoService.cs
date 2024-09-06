using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;

public interface IRelatorioAplicacaoService
{
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync();
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllByIdEmpresaAsync(string? idEmpresa);
    Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id);
    Task<RelatorioAplicacaoViewModel> AddAsync(RelatorioAplicacaoViewModel obj, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataCriacaoAsync(DateTime Date, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusAsync(string? idEmpresa);
    Task<ExportRelatorioViewModel> ExportExcelAsync(int? id);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids, int isMapa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaAsync(string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetByDataAlteracaoAsync(DateTime Date, string? idEmpresa);
    Task<IEnumerable<RelatorioAplicacaoViewModel>> GetNovosAsync(DateTime? date, string? userId);
    Task UpdateAsync(RelatorioAplicacaoViewModel obj);
    Task UpdateIsMapaAsync(List<int> obj, bool condicao);
    Task DeleteAsync(int id);
}
