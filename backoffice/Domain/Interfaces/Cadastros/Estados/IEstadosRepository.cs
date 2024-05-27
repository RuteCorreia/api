using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Estados;

public interface IEstadosRepository
{
    Task AddAsync(Entidades.Cadastros.Estados.Estados obj);
    Task UpdateAsync(Entidades.Cadastros.Estados.Estados obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Estados.Estados>> GetAllAsync();
    Task<Entidades.Cadastros.Estados.Estados> GetByIdAsync(int id);
}
