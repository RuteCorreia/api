using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoContrato;

public class AplicacaoContratoRepository : IAplicacaoContratoRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoContratoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoContrato.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoContrato.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato obj)
    {
        _contextBase.AplicacaoContrato.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
