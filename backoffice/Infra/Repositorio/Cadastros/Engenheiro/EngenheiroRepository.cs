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

    public async Task<IEnumerable<UsuarioCredencial>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.UsuarioCredencial
            .AsNoTracking()
            .Where(x => 
                x.Usuario != null 
                && !x.Usuario.Removido 
                && x.Funcao == ERole.EngAgronomoCoord
                && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa))
            .Include(u => u.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<UsuarioCredencial?> GetByIdAsync(string id, int idEmpresa)
    {
        var obj = await _contextBase.UsuarioCredencial
            .Where(x => 
                x.Funcao == ERole.EngAgronomoCoord 
                && x.Usuario != null
                && !x.Usuario.Removido 
                && x.IdUsuario == Guid.Parse(id)
                && (idEmpresa == 0 ? x.Usuario.IdEmpresa == null : x.Usuario.IdEmpresa == idEmpresa))
            .Include(u => u.Usuario)
            .FirstOrDefaultAsync();
        return obj;
    }
}
