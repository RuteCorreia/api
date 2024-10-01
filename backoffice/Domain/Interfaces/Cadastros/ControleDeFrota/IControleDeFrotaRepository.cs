using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.ControleDeFrota;

public interface IControleDeFrotaRepository
{
    Task<int> AddAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task<int?> UpdateAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(DateTime? offsetDate, string userName);
    Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
    Task<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int? id);
}
