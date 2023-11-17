using Domain.Interfaces.Cadastros.AlvoBiologico;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AlvoBiologico;

public class AlvoBiologicoRepository : IAlvoBiologicoRepository
{
    private readonly ContextBase _contextBase;

    public AlvoBiologicoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = GetByIdAsync(id);
        if(ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync()
    {
        var entities = await _contextBase.AlvoBiologico.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AlvoBiologico.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj)
    {
        _contextBase.AlvoBiologico.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
