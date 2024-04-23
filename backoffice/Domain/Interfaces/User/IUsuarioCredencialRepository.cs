using Domain.Entidades.User;

namespace Domain.Interfaces.User;

public interface IUsuarioCredencialRepository
{
    Task AddListAsync(IEnumerable<UsuarioCredencial> obj);

    Task<IEnumerable<UsuarioCredencial>> GetUsuarioCredencialsAsync(Guid userId);

    Task RemoveAllByUserIdAsync(Guid userId);

    Task<bool> VerificarSeEmpresaPossuiEngenheiroAtivo(int? idEmpresa, string idUsuario = "");
}
