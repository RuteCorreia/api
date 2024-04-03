using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(string id);
}
