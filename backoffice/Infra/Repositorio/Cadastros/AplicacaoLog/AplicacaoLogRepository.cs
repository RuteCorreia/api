using Domain.Interfaces.Cadastros.AplicacaoLog;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoLog;

public class AplicacaoLogRepository : IAplicacaoLogRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoLogRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoLog.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoLog.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog obj)
    {
        var objeto = await _contextBase.AplicacaoLog.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.Data = obj.Data;
        objeto.Nome = obj.Nome;
        objeto.Descricao = obj.Descricao;

        _contextBase.AplicacaoLog.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
