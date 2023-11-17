using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoAreaTratada;

public class AplicacaoAreaTratadaRepository : IAplicacaoAreaTratadaRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoAreaTratadaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoAreaTratada.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoAreaTratada.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj)
    {
        _contextBase.AplicacaoAreaTratada.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
