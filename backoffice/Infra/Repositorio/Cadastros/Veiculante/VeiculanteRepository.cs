using Domain.Interfaces.Cadastros.Veiculante;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Veiculante;

public class VeiculanteRepository : IVeiculanteRepository
{
    private readonly ContextBase _contextBase;

    public VeiculanteRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Veiculante.Veiculante obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Veiculante.Veiculante>> GetAllAsync()
    {
        var entities = await _contextBase.Veiculante.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Veiculante.Veiculante> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Veiculante.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Veiculante.Veiculante obj)
    {
        _contextBase.Veiculante.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
