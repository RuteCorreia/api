using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task AddAsync(Entidades.Cadastros.Executor.Executor obj);
    Task UpdateAsync(Entidades.Cadastros.Executor.Executor obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Executor.Executor>> GetAllAsync();
    Task<Entidades.Cadastros.Executor.Executor> GetByIdAsync(int id);
}
