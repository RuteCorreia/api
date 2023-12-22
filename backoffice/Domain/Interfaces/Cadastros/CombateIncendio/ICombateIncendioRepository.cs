using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.CombateIncendio;

public interface ICombateIncendioRepository
{
    Task AddAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task UpdateAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync();
    Task<Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id);
}
