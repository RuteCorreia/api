using Domain.Interfaces.Cadastros.Pista;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Pistas;

public class PistaRepository : IPistaRepository
{
    private readonly ContextBase _contextBase;

    public PistaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.Pistas.Pista obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Pistas.Pista>> GetAllAsync()
    {
        var entities = await _contextBase.Pista.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Pistas.Pista> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Pista.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Pistas.Pista obj)
    {
        var objeto = await _contextBase.Pista.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;
        objeto.LAT = obj.LAT;
        objeto.LONG = obj.LONG;

        _contextBase.Pista.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
