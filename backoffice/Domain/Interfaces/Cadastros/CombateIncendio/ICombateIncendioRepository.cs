using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.CombateIncendio;

public interface ICombateIncendioRepository
{
    Task<int> AddAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task<int> UpdateAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync(DateTime? offsetDate, int idEmpresa);
    Task<Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id);
}
