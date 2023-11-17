using Domain.Interfaces.Cadastros.Adjuvante;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Adjuvante;

public class AdjuvanteRepository : IAdjuvanteRepository
{
    private readonly ContextBase _contextBase;

    public AdjuvanteRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Adjuvante.Adjuvante obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Adjuvante.Adjuvante>> GetAllAsync()
    {
        var entities = await _contextBase.Adjuvante.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Adjuvante.Adjuvante> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Adjuvante.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Adjuvante.Adjuvante obj)
    {
        _contextBase.Adjuvante.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
