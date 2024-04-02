using Domain.Entidades.User;
using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task AddAsync(Usuario obj);
    Task UpdateAsync(Usuario obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(string id);
    Task<Usuario> GetByLoginAsync(string email, string password);
}
