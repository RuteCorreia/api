using Domain.Interfaces.Cadastros.Menu;
using Helpers;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Empresa;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Menu;

public class MenuRepository : IMenuRepository
{
    private readonly ContextBase _contextBase;
    public MenuRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Menu.Menu obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if (!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Menu.Menu>> GetAllAsync(int idEmpresa, IEnumerable<string>? roleNames)
    {
        var empresa = await _contextBase.Empresa.FindAsync(idEmpresa);
        IEnumerable<Domain.Entidades.Cadastros.Menu.Menu> entities = null;

        if (roleNames != null && roleNames.Contains("Administrador") && empresa != null && empresa.Manutencao == true)
        {
            entities = await _contextBase.Menu
                .AsNoTracking()
                .Where(x => x.MenuItemId != 4)
                .ToListAsync();

        }
        else if (roleNames != null && roleNames.Contains("Administrador") && empresa != null && empresa.Manutencao == false)
        {
            entities = await _contextBase.Menu
                .AsNoTracking()
                .Where(x => x.MenuItemId != 4)
                .ToListAsync();
        }
        else if (empresa != null && empresa.Manutencao == true)
        {
            entities = await _contextBase.Menu
                .AsNoTracking()
                .Where(x => x.MenuItemId != 1005 && x.MenuItemId != 4)
                .ToListAsync();
        }
        else
        {
            entities = await _contextBase.Menu
                .AsNoTracking()
                .Where(x => x.MenuItemId != 4 && x.MenuItemId != 1005)
                .ToListAsync();
        }
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Menu.Menu> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Menu.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Menu.Menu obj)
    {
        var objeto = await _contextBase.Menu.FindAsync(obj.MenuItemId);
        objeto.Label = obj.Label;
        objeto.Route = obj.Route;
        objeto.Icon = obj.Icon;

        _contextBase.Menu.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
