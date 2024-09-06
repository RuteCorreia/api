using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.ExportExcel.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendio.Interface;

public interface ICombateIncendioService 
{
    Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync(DateTime? offsetDate, string? userId);

    Task<CombateIncendioViewModel> GetByIdAsync(int id);

    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusAsync(string? idEmpresa);
    
    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaAsync(string? idEmpresa);

    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<CombateIncendioViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<CombateIncendioViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids);

    Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa);

    Task<int> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa);
    Task<ExportRelatorioViewModel> ExportExcelAsync(int? id);
    Task DeleteAsync(int id);
    Task UpdateIsMapaAsync(List<int> obj, bool condicao);
}
