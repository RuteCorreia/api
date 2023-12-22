using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aeronave;

public interface IAeronaveRepository
{
    Task AddAsync(Entidades.Cadastros.Aeronave.Aeronave obj);
    Task UpdateAsync(Entidades.Cadastros.Aeronave.Aeronave obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aeronave.Aeronave>> GetAllAsync();
    Task<Entidades.Cadastros.Aeronave.Aeronave> GetByIdAsync(int id);
}
