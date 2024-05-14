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

    public async Task<IEnumerable<UsuarioCredencial>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => 
                x.Usuario != null 
                && !x.Usuario.Removido 
                && x.Funcao == ERole.TecnicoExecutor
                && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa))
            .Include(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => 
                x.Funcao == ERole.TecnicoExecutor
                && x.Usuario != null
                && !x.Usuario.Removido
                && x.IdUsuario == Guid.Parse(id)
                && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa))
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync();

        return obj;
    }
}
