using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Engenheiro;

public interface IEngenheiroRepository
{
    Task<IEnumerable<UsuarioCredencial>> GetAllAsync();
    Task<UsuarioCredencial?> GetByIdAsync(string id);
}
