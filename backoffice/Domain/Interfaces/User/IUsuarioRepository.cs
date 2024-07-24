using Domain.Entidades.User;

namespace Domain.Interfaces.User;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario obj);
    Task UpdateAsync(Usuario obj);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Usuario>> GetAllAsync(int? idEmpresa);
    Task<Usuario> GetUserByEmailAsync(string email);
    Task<Usuario> GetByUserIdAsync(string id);
    Task<Usuario> GetLastAsync();
    Task<Usuario> GetUserByIdAsync(string id);
}