using Domain.Interfaces.Cadastros.AlturaVoo;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AlturaVoo;

public class AlturaVooRepository : IAlturaVooRepository
{
    private readonly ContextBase _contextBase;

    public AlturaVooRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>> GetByDateAsync(DateTime dataUltimaSincronizacao)
    {
        return await _contextBase.AlturaVoo
            .Where(av => av.DataSituacao > dataUltimaSincronizacao)
            .ToListAsync();
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo>> GetAllAsync()
    {
        var entities = await _contextBase.AlturaVoo.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AlturaVoo.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Altura_Voo.AlturaVoo obj)
    {
        var objeto = await _contextBase.AlturaVoo.FindAsync(obj.Id);
        objeto.Nome = obj.Nome;

        _contextBase.AlturaVoo.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
