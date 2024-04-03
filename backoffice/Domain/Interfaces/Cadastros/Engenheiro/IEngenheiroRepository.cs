using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Engenheiro;

public interface IEngenheiroRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(string id);
}
