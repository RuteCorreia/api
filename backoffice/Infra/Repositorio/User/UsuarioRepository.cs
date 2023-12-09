using Domain.Entidades.User;
using Domain.Interfaces.User;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.User;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ContextBase _contextBase;

    public UsuarioRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Usuario obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> GetByUserIdAsync(string id)
    {
        var usuario = await _contextBase.Usuario.FirstOrDefaultAsync(x => string.Equals(x.UserId, id));
        return usuario;
    }

    public async Task UpdateAsync(Usuario obj)
    {
        throw new NotImplementedException();
    }
}
