using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Engenheiro;

public interface IEngenheiroRepository
{
    Task AddAsync(Usuario obj);
    Task UpdateAsync(Usuario obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(string id);
    Task<Usuario> GetByLoginAsync(string email, string password);
    Task<Usuario> GetByIdEmpresaAsync(int id, int idEmpresa);
}
