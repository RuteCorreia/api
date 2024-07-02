using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;

public interface ICombateIncendioDecolagemPousoRepository
{
    Task<int> AddAsync(Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj);
    Task UpdateAsync(Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>> GetAllAsync();
    Task<Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> GetByIdAsync(int id);
}
