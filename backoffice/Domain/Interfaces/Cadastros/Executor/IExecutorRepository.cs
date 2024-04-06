using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task<IEnumerable<UsuarioCredencial>> GetAllAsync();
    Task<UsuarioCredencial?> GetByIdAsync(string id);
}
