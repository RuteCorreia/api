using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Veiculante;

public interface IVeiculanteRepository
{
    Task AddAsync(Entidades.Cadastros.Veiculante.Veiculante obj);
    Task UpdateAsync(Entidades.Cadastros.Veiculante.Veiculante obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Veiculante.Veiculante>> GetAllAsync();
    Task<Entidades.Cadastros.Veiculante.Veiculante> GetByIdAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Veiculante.Veiculante>> GetByDateAsync(DateTime dataUltimaSincronizacao);
}
