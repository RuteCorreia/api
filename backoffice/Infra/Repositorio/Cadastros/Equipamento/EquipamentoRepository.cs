using Domain.Interfaces.Cadastros.Equipamento;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Equipamento;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly ContextBase _contextBase;

    public EquipamentoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Equipamento.Equipamento obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Equipamento.Equipamento>> GetAllAsync()
    {
        var entities = await _contextBase.Equipamento.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Equipamento.Equipamento> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Equipamento.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Equipamento.Equipamento obj)
    {
        _contextBase.Equipamento.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
