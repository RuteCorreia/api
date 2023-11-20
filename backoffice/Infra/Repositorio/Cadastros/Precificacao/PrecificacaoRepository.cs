using Domain.Interfaces.Cadastros.Precificacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Precificacao;

public class PrecificacaoRepository : IPrecificacaoRepository
{
    private readonly ContextBase _contextBase;

    public PrecificacaoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Precificacao.Precificacao obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Precificacao.Precificacao>> GetAllAsync()
    {
        var entities = await _contextBase.Precificacao.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Precificacao.Precificacao> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Precificacao.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Precificacao.Precificacao obj)
    {
        _contextBase.Precificacao.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
