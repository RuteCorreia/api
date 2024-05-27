using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task<IEnumerable<UsuarioCredencial>> GetAllAsync(int idEmpresa);
    Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa);
}
