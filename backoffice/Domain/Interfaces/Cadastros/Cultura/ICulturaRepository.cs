using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Cultura;

public interface ICulturaRepository
{
    Task AddAsync(Entidades.Cadastros.Cultura.Cultura obj);
    Task UpdateAsync(Entidades.Cadastros.Cultura.Cultura obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Cultura.Cultura>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.Cultura.Cultura> GetByIdAsync(int id);
    Task<Entidades.Cadastros.Cultura.Cultura> GetByNameAsync(string name, int idEmpresa);
}
