using Domain.Entidades.User;

namespace Domain.Interfaces.Cadastros.Piloto;

public interface IPilotoRepository
{
    Task<IEnumerable<Domain.Entidades.Cadastros.Piloto.Piloto>> GetAllAsync(int idEmpresa);
    Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa);
}
