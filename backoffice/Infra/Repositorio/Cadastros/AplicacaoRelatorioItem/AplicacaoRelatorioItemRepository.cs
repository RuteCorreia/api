using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoRelatorioItem;

public class AplicacaoRelatorioItemRepository : IAplicacaoRelatorioItemRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoRelatorioItemRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoRelatorioItem.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRelatorioItem.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj)
    {
        _contextBase.AplicacaoRelatorioItem.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
