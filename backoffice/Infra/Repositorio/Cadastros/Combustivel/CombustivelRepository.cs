using Domain.Interfaces.Cadastros.Combustivel;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Combustivel;

public class CombustivelRepository : ICombustivelRepository
{
    private readonly ContextBase _contextBase;

    public CombustivelRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Combustivel.Combustivel obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Combustivel.Combustivel>> GetAllAsync()
    {
        var entities = await _contextBase.Combustivel.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Combustivel.Combustivel> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Combustivel.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Combustivel.Combustivel obj)
    {
        _contextBase.Combustivel.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
