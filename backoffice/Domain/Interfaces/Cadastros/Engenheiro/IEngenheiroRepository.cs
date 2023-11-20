using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Engenheiro;

public interface IEngenheiroRepository
{
    Task AddAsync(Entidades.Cadastros.Engenheiro.Engenheiro obj);
    Task UpdateAsync(Entidades.Cadastros.Engenheiro.Engenheiro obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Engenheiro.Engenheiro>> GetAllAsync();
    Task<Entidades.Cadastros.Engenheiro.Engenheiro> GetByIdAsync(int id);
}
