using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Produto;
using Domain.Interfaces.Cadastros.Produto;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
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

    public async Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids)
    {
        var query = @"SELECT Nome FROM Produto WHERE Id IN @Ids";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Ids = ids };
            var result = await connection.QueryAsync<string>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe)
    {
        var nomes = await _contextBase.Produto
            .Where(p => p.Classe == classe)
            .ToListAsync();
        return nomes;
    }

    public async Task<Domain.Entidades.Cadastros.Produto.Produto> GetByNameAsync(string nome)
    {
        var produto = await _contextBase.Produto
            .FirstOrDefaultAsync(p => p.Nome == nome);
        return produto;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetAllAsync(string? nomeProduto)
    {
        var query = _contextBase.Produto.AsQueryable();

        // Aplica o filtro no nome apenas se o parâmetro nomeProduto não for vazio
        if (!string.IsNullOrEmpty(nomeProduto))
        {
            query = query.Where(p => p.Nome.Contains(nomeProduto));
        }

        var entities = await query.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Produto.Produto> GetByIdAsync(int? id)
    {
        var obj = await _contextBase.Produto.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Produto.Produto obj)
    {
        var objeto = await _contextBase.Produto.FindAsync(obj.Id);
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.IdTipoDeFormulacao = obj.IdTipoDeFormulacao;
        objeto.IdTipoDeServico = obj.IdTipoDeServico;

        _contextBase.Produto.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
