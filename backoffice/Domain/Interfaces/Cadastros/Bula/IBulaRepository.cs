using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Bula;

public interface IBulaRepository
{
    Task AddAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task UpdateAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Bula>> GetAllAsync();
    Task<Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id);
    Task<Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name);
}
