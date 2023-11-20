using Domain.Interfaces.Cadastros.Produto;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Produto;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ContextBase _contextBase;

    public ProdutoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Produto.Produto obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetAllAsync()
    {
        var entities = await _contextBase.Produto.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Produto.Produto> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Produto.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Produto.Produto obj)
    {
        _contextBase.Produto.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
