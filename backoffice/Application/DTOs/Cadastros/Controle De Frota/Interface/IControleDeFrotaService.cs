using Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

namespace Application.DTOs.Cadastros.Controle_De_Frota.Interface;

public interface IControleDeFrotaService 
{
    Task<IEnumerable<ControleDeFrotaViewModel>> GetAllAsync(DateTime? offsetDate, string? userId, IEnumerable<string>? roleNames, string? idEmpresa);

    Task<ControleDeFrotaViewModel> GetByIdAsync(int? id);

    Task<IEnumerable<ControleDeFrotaViewModel>> GetListByMesAsync(string? idEmpresa, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<ControleDeFrotaViewModel>> GetListByIdsAsync(string? idEmpresa, List<int> ids, int isMapa);

    Task<int> AddAsync(ControleDeFrotaViewModel obj, string? idEmpresa);
    Task<IEnumerable<ControleDeFrotaViewModel>> GetListByStatusAsync(string? idEmpresa);

    Task<int?> UpdateAsync(ControleDeFrotaViewModel obj);

    Task DeleteAsync(int id);
}
