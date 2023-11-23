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
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
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
        var objeto = await _contextBase.Executor.FindAsync(obj.IdExecutor);
        objeto.IdEmpresa = obj.IdEmpresa;
        objeto.Nome = obj.Nome;
        objeto.Email = obj.Email;
        objeto.Senha = obj.Senha;
        objeto.CFTA = obj.CFTA;
        objeto.Assinatura = obj.Assinatura;

        _contextBase.Executor.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
