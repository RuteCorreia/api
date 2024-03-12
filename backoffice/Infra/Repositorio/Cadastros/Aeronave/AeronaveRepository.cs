using Domain.Interfaces.Cadastros.Aeronave;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Aeronave;

public class AeronaveRepository : IAeronaveRepository
{
    private readonly ContextBase _contextBase;

    public AeronaveRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aeronave.Aeronave obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aeronave.Aeronave>> GetAllAsync()
    {
        var entities = await _contextBase.Aeronave.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aeronave.Aeronave> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Aeronave.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aeronave.Aeronave obj)
    {
        var objeto = await _contextBase.Aeronave.FindAsync(obj.Id);
        objeto.Fabricante = obj.Fabricante;
        objeto.Prefixo = obj.Prefixo;
        objeto.Modelo = obj.Modelo;
        objeto.SerialNumber = obj.SerialNumber;

        _contextBase.Aeronave.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
