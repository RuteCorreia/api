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

    public async Task<Usuario> GetUserByEmailAsync(string email)
    {
        return await _contextBase.Usuario.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task DeleteAsync(Guid id)
    {
        var usuario = _contextBase.Usuario.Where(x => x.Id == id).FirstOrDefault();
        if (usuario != null) 
        {
            _contextBase.Remove(usuario);
            await _contextBase.SaveChangesAsync();
        } 
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync(int? idEmpresa)
    {
       return await _contextBase.Usuario
            .AsNoTracking()
            .Where(x => !x.Removido && x.IdEmpresa == idEmpresa)
            .ToListAsync();
    }

    public async Task<Usuario> GetUserByIdAsync(string id) => await _contextBase.Usuario.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

    public async Task<Usuario> GetByUserIdAsync(string id) => await _contextBase.Usuario.Include("Empresa").FirstOrDefaultAsync(x => string.Equals(x.UserId, id) && !x.Removido);

    public async Task<Usuario> GetLastAsync() => await _contextBase.Usuario
        .AsNoTracking()
        .Where(x => !x.Removido)
        .OrderBy(x => x.NrUsuario)
        .LastOrDefaultAsync();
    

    public async Task UpdateAsync(Usuario obj)
    {
        _contextBase.Update(obj);
        await _contextBase.SaveChangesAsync();
    }

}
