using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.ControleDeFrota;

public interface IControleDeFrotaRepository
{
    Task AddAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task UpdateAsync(Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync();
    Task<Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int id);
}
