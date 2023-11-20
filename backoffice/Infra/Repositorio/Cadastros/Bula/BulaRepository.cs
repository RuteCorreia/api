using Domain.Interfaces.Cadastros.Bula;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Bula;

public class BulaRepository : IBulaRepository
{
    private readonly ContextBase _contextBase;

    public BulaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Bula>> GetAllAsync()
    {
        var entities = await _contextBase.Bula.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Bula.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
    {
        _contextBase.Bula.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
