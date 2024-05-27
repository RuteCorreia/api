using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.CombateIncendioDecolagemPouso;

public class CombateIncendioDecolagemPousoRepository : ICombateIncendioDecolagemPousoRepository
{
    private readonly ContextBase _contextBase;

    public CombateIncendioDecolagemPousoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>> GetAllAsync()
    {
        var entities = await _contextBase.CombateIncendioDecolagemPouso.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> GetByIdAsync(int id)
    {
        var obj = await _contextBase.CombateIncendioDecolagemPouso.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj)
    {
        var objeto = await _contextBase.CombateIncendioDecolagemPouso.FindAsync(obj.Id);
        objeto.IdCombateIncendio = obj.IdCombateIncendio;
        objeto.DecolagemHorario = obj.DecolagemHorario;
        objeto.DecolagemHorimetro = obj.DecolagemHorimetro;
        objeto.PousoHorario = obj.PousoHorario;
        objeto.PousoHorimetro = obj.PousoHorimetro;

        _contextBase.CombateIncendioDecolagemPouso.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
