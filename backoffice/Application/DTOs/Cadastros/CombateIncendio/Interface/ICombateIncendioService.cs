using Application.DTOs.Cadastros.CombateIncendio.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendio.Interface;

public interface ICombateIncendioService 
{
    Task<IEnumerable<CombateIncendioViewModel>> GetAllAsync(DateTime? offsetDate, string? idEmpresa);

    Task<CombateIncendioViewModel> GetByIdAsync(int id);

    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusAsync(string? idEmpresa);
    
    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaAsync(string? idEmpresa);

    Task<IEnumerable<CombateIncendioViewModel>> GetListByStatusMapaMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);

    Task<int> AddAsync(CombateIncendioViewModel obj, string? idEmpresa);

    Task<int> UpdateAsync(CombateIncendioViewModel obj, string? idEmpresa);

    Task DeleteAsync(int id);
    Task UpdateIsMapaAsync(List<int> obj);
}
