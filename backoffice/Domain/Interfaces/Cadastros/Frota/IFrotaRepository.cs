using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Frota;

public interface IFrotaRepository
{
    Task AddAsync(Entidades.Cadastros.Frota.Frota obj);
    Task UpdateAsync(Entidades.Cadastros.Frota.Frota obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Frota.Frota>> GetAllAsync();
    Task<Entidades.Cadastros.Frota.Frota> GetByIdAsync(int id);
}
