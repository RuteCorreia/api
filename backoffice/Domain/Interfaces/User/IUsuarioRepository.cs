using Domain.Entidades.User;

namespace Domain.Interfaces.User;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario obj);
    Task UpdateAsync(Usuario obj);
    Task DeleteAsync(string id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByUserIdAsync(string id);
    Task<Usuario> GetLastAsync();
    Task<Usuario> GetUserByIdAsync(string id);
}