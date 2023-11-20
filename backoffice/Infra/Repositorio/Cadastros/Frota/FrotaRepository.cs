using Domain.Interfaces.Cadastros.Frota;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Frota;

public class FrotaRepository : IFrotaRepository
{
    private readonly ContextBase _contextBase;

    public FrotaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Frota.Frota obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Frota.Frota>> GetAllAsync()
    {
        var entities = await _contextBase.Frota.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Frota.Frota> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Frota.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Frota.Frota obj)
    {
        _contextBase.Frota.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
