using Domain.Interfaces.Cadastros.PlanoDeContrato;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.PlanoDeContrato;

public class PlanoDeContratoRepository : IPlanoDeContratoRepository
{
    private readonly ContextBase _contextBase;

    public PlanoDeContratoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.PlanoDeContrato obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.PlanoDeContrato>> GetAllAsync()
    {
        var entities = await _contextBase.PlanoDeContrato.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.PlanoDeContrato> GetByIdAsync(int id)
    {
        var obj = await _contextBase.PlanoDeContrato.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.PlanoDeContrato obj)
    {
        var objeto = await _contextBase.PlanoDeContrato.FindAsync(obj.IdPlano);
        objeto.NomeDoPlano = obj.NomeDoPlano;

        _contextBase.PlanoDeContrato.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
