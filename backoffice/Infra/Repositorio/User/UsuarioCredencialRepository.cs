using Domain.Entidades.User;
using Domain.Interfaces.User;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.User;

public class UsuarioCredencialRepository : IUsuarioCredencialRepository
{
    private readonly ContextBase _contextBase;

    public UsuarioCredencialRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddListAsync(IEnumerable<UsuarioCredencial> obj)
    {
        await _contextBase.UsuarioCredencial.AddRangeAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<IEnumerable<UsuarioCredencial>> GetUsuarioCredencialsAsync(Guid userId)
    {
        return await _contextBase.UsuarioCredencial
            .Where(x => x.IdUsuario == userId)
            .ToListAsync();
    }

    public async Task RemoveAllByUserIdAsync(Guid userId)
    {
        var listToRemove = await GetUsuarioCredencialsAsync(userId);
        _contextBase.UsuarioCredencial.RemoveRange(listToRemove);
        await _contextBase.SaveChangesAsync();
    }

}
