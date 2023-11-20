using Domain.Interfaces.Cadastros.Engenheiro;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Engenheiro;

public class EngenheiroRepository : IEngenheiroRepository
{
    private readonly ContextBase _contextBase;

    public EngenheiroRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Engenheiro.Engenheiro obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>> GetAllAsync()
    {
        var entities = await _contextBase.Engenheiro.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Engenheiro.Engenheiro> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Engenheiro.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Engenheiro.Engenheiro obj)
    {
        _contextBase.Engenheiro.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
