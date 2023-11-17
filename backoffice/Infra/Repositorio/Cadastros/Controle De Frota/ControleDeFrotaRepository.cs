using Domain.Interfaces.Cadastros.ControleDeFrota;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Controle_De_Frota;

public class ControleDeFrotaRepository : IControleDeFrotaRepository
{
    private readonly ContextBase _contextBase;

    public ControleDeFrotaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync()
    {
        var entities = await _contextBase.ControleDeFrota.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int id)
    {
        var obj = await _contextBase.ControleDeFrota.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        _contextBase.ControleDeFrota.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
