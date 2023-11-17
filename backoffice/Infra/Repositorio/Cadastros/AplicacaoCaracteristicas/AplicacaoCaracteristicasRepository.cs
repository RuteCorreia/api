using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoCaracteristicas;

public class AplicacaoCaracteristicasRepository : IAplicacaoCaracteristicasRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoCaracteristicasRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoCaracteristicas.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoCaracteristicas.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj)
    {
        _contextBase.AplicacaoCaracteristicas.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
