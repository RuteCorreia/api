using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Executor;

public interface IExecutorRepository
{
    Task<IEnumerable<Entidades.Cadastros.Executor.Executor>> GetAllAsync(int idEmpresa);
    Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Executor.Executor>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
}
