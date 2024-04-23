using Domain.Entidades.User;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Executor;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Executor;

public class ExecutorRepository : IExecutorRepository
{
    private readonly ContextBase _contextBase;

    public ExecutorRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<UsuarioCredencial>> GetAllAsync()
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => 
                x.Usuario != null 
                && !x.Usuario.Removido 
                && x.Funcao == ERole.TecnicoExecutor)
            .Include(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<UsuarioCredencial?> GetByIdAsync(string id)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => 
                x.Funcao == ERole.TecnicoExecutor
                && x.Usuario != null
                && !x.Usuario.Removido
                && x.IdUsuario == Guid.Parse(id))
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync();

        return obj;
    }
}
