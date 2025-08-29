using Domain.Entidades.Cadastros.Atividade;

namespace Domain.Interfaces.Cadastros.ControleDeFrota;

public interface IControleDeFrotaRepository
{
    Task<int> AddAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task<int?> UpdateAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(DateTime? offsetDate, string userName, IEnumerable<string>? roleNames, int IdEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByIdsAsync(List<int> ids, int idEmpresa, int statusEnvio, int isMapa);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByIdsAsync(List<int> ids);
    Task<IEnumerable<Atividade>> GetAtividadesByFiltrosAsync(AtividadeFiltro atividadeFiltro);
    Task<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int? id);
}
