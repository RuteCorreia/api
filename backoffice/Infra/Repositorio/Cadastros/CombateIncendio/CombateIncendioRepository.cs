using Domain.Interfaces.Cadastros.CombateIncendio;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.CombateIncendio;

public class CombateIncendioRepository : ICombateIncendioRepository
{
    private readonly ContextBase _contextBase;

    public CombateIncendioRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync()
    {
        var entities = await _contextBase.CombateIncendio.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id)
    {
        var obj = await _contextBase.CombateIncendio.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
    {
        _contextBase.CombateIncendio.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
