using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Adjuvante;

public interface IAdjuvanteRepository
{
    Task AddAsync(Entidades.Cadastros.Adjuvante.Adjuvante obj);
    Task UpdateAsync(Entidades.Cadastros.Adjuvante.Adjuvante obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Adjuvante.Adjuvante>> GetAllAsync();
    Task<Entidades.Cadastros.Adjuvante.Adjuvante> GetByIdAsync(int id);
}
