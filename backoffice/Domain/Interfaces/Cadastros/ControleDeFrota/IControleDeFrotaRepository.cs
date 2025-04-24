using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.ControleDeFrota;

public interface IControleDeFrotaRepository
{
    Task<int> AddAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task<int?> UpdateAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(DateTime? offsetDate, string userName, IEnumerable<string>? roleNames, int IdEmpresa);
    Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
    Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByIdsAsync(List<int> ids, int idEmpresa, int statusEnvio, int isMapa);
    Task<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int? id);
}
