using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Piloto;

public interface IPilotoRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(string id);
}
