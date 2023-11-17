using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Combustivel;

public interface ICombustivelRepository 
{
    Task AddAsync(Entidades.Cadastros.Combustivel.Combustivel obj);
    Task UpdateAsync(Entidades.Cadastros.Combustivel.Combustivel obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Combustivel.Combustivel>> GetAllAsync();
    Task<Entidades.Cadastros.Combustivel.Combustivel> GetByIdAsync(int id);
}
