using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Pista;

public interface IPistaRepository
{
    Task<int> AddAsync(Entidades.Cadastros.Pistas.Pista obj);
    Task UpdateAsync(Entidades.Cadastros.Pistas.Pista obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Pistas.Pista>> GetAllAsync();
    Task<Entidades.Cadastros.Pistas.Pista> GetByIdAsync(int id);
}
