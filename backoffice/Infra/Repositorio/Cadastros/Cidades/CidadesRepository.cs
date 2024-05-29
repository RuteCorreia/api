using Domain.Interfaces.Cadastros.Cidades;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Cidades;

public class CidadesRepository : ICidadeRepository
{
    private readonly ContextBase _contextBase;

    public CidadesRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Cidades.Cidades obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Cidades.Cidades>> GetAllAsync()
    {
        var entities = await _contextBase.Cidades.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Cidades.Cidades> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Cidades.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Cidades.Cidades obj)
    {
        var objeto = await _contextBase.Cidades.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;
        objeto.Sigla = obj.Sigla;

        _contextBase.Cidades.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
