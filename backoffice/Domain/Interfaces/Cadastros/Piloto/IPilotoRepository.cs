using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Piloto;

public interface IPilotoRepository
{
    Task<IEnumerable<UsuarioCredencial>> GetAllAsync();
    Task<UsuarioCredencial?> GetByIdAsync(string id);
}
