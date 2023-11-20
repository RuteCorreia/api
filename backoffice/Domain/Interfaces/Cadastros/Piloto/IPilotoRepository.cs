using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Piloto;

public interface IPilotoRepository
{
    Task AddAsync(Entidades.Cadastros.Piloto.Piloto obj);
    Task UpdateAsync(Entidades.Cadastros.Piloto.Piloto obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Piloto.Piloto>> GetAllAsync();
    Task<Entidades.Cadastros.Piloto.Piloto> GetByIdAsync(int id);
}
