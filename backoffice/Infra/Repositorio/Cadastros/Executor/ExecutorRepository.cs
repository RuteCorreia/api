using Domain.Interfaces.Cadastros.Executor;
using Helpers;
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

    public async Task AddAsync(Domain.Entidades.Cadastros.Executor.Executor obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Executor.Executor>> GetAllAsync()
    {
        var entities = await _contextBase.Executor.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Executor.Executor> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Executor.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Executor.Executor obj)
    {
        _contextBase.Executor.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
