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
        var usuario = await _contextBase.Usuario.FindAsync(id);
        _contextBase.Usuario.Remove(usuario);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
       return _contextBase.Usuario.ToList();
    }

    public async Task<Usuario> GetUserByIdAsync(string id) => _contextBase.Usuario.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

    public async Task<Usuario> GetByUserIdAsync(string id) => await _contextBase.Usuario.FirstOrDefaultAsync(x => string.Equals(x.UserId, id));
    

    public async Task<Usuario> GetLastAsync() => await _contextBase.Usuario
        .AsNoTracking()
        .Where(x => !x.Removido)
        .OrderBy(x => x.NrUsuario)
        .LastOrDefaultAsync();
    

    public async Task UpdateAsync(Usuario obj)
    {
        throw new NotImplementedException();
    }
}
