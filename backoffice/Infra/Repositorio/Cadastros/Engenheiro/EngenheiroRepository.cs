using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Engenheiro;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Engenheiro;

public class EngenheiroRepository : IEngenheiroRepository
{
    private readonly ContextBase _contextBase;

    public EngenheiroRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => x.Usuario != null && !x.Usuario.Removido && x.Funcao == ERole.EngAgronomoCoord)
            .Select(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<Usuario> GetByIdAsync(string id)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => x.Funcao == ERole.EngAgronomoCoord && x.IdUsuario == Guid.Parse(id))
            .Select(u => u.Usuario)
            .FirstOrDefaultAsync();
        return obj;
    }
}
