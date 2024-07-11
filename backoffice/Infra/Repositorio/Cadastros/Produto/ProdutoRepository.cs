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
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<string>> GetClasses()
    {
        var classes = await _contextBase.Produto
            .Select(p => p.Classe)
            .Distinct()
            .ToListAsync();
        return classes;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe)
    {
        var nomes = await _contextBase.Produto
            .Where(p => p.Classe == classe)
            .ToListAsync();
        return nomes;
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
        var objeto = await _contextBase.Produto.FindAsync(obj.Id);
        objeto.IdCultura = obj.IdCultura;
        objeto.Nome = obj.Nome;
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.Classe = obj.Classe;
        objeto.TipoDeFormulacao = obj.TipoDeFormulacao;
        objeto.TipoServico = obj.TipoServico;

        _contextBase.Produto.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
