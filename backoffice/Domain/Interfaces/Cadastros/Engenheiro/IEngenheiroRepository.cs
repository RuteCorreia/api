using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Engenheiro;

public interface IEngenheiroRepository
{
    Task<IEnumerable<UsuarioCredencial>> GetAllAsync(int idEmpresa);
    Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa);
}
