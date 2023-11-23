using Domain.Interfaces.Cadastros.Estados;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Estados;

public class EstadosRepository : IEstadosRepository
{
    private readonly ContextBase _contextBase;

    public EstadosRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Estados.Estados obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Estados.Estados>> GetAllAsync()
    {
        var entities = await _contextBase.Estados.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Estados.Estados> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Estados.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Estados.Estados obj)
    {
        var objeto = await _contextBase.Estados.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;
        objeto.Sigla = obj.Sigla;

        _contextBase.Estados.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
