using Domain.Entidades.Cadastros.Empresa;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Equipamento.Equipamento>> GetByDateAsync(DateTime dataUltimaSincronizacao)
    {
        var entities = await _contextBase.Equipamento
            .Where(e => e.DataSituacao > dataUltimaSincronizacao)
            .ToListAsync();
        return entities;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Equipamento.Equipamento obj)
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
        var objeto = await _contextBase.Equipamento.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;

        _contextBase.Equipamento.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
