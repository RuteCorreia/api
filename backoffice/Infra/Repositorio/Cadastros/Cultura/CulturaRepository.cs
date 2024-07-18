using Domain.Interfaces.Cadastros.Cultura;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Cultura;

public class CulturaRepository : ICulturaRepository
{
    private readonly ContextBase _contextBase;

    public CulturaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Cultura.Cultura obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cultura.Cultura>> GetAllAsync()
    {
        var entities = await _contextBase.Cultura.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Cultura.Cultura> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Cultura.FindAsync(id);
        return obj;
    }

    public async Task<Domain.Entidades.Cadastros.Cultura.Cultura> GetByNameAsync(string name)
    {
        var obj = await _contextBase.Cultura.Where(x => x.Nome == name).FirstOrDefaultAsync();
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Cultura.Cultura obj)
    {
        var objeto = await _contextBase.Cultura.FindAsync(obj.IdCultura);
        objeto.Nome = obj.Nome;
        objeto.AlvoBiologico = obj.AlvoBiologico;

        _contextBase.Cultura.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
