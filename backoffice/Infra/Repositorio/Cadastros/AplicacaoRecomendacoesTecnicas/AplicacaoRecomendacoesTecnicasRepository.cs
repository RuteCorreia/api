using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoRecomendacoesTecnicas;

public class AplicacaoRecomendacoesTecnicasRepository : IAplicacaoRecomendacoesTecnicasRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoRecomendacoesTecnicasRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoRecomendacoesTecnicas.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRecomendacoesTecnicas.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj)
    {
        _contextBase.AplicacaoRecomendacoesTecnicas.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
