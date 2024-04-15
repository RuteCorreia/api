namespace Domain.Interfaces.Cadastros.ManutencaoAeronave;

public interface IManutencaoAeronaveRepository
{
    Task<Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave> AddAsync(Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj);
    Task UpdateAsync(Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>> GetAllAsync();
    Task<Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave> GetByIdAsync(int id);
}
