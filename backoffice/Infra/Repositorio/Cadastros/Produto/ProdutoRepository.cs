using Dapper;
using Domain.Entidades.Cadastros.Empresa;
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

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<string>> GetClasses(int idEmpresa)
    {
        var classes = await _contextBase.Produto
            .Where(p => p.IdEmpresa == idEmpresa || p.IdEmpresa == 196)
            .Select(p => p.Classe)
            .Distinct()
            .ToListAsync();
        return classes;
    }

    public async Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids, int idEmpresa)
    {
        var query = @"SELECT Nome 
                      FROM Produto 
                      WHERE Id IN @Ids
                      AND IdEmpresa IN (@IdEmpresa)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Ids = ids, IdEmpresa = idEmpresa };
            var result = await connection.QueryAsync<string>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe, int idEmpresa)
    {
        var nomes = await _contextBase.Produto
            .Where(p => p.Classe == classe && (p.IdEmpresa == idEmpresa))
            .OrderBy(p => p.Nome)
            .ToListAsync();
        return nomes;
    }

    public async Task<Domain.Entidades.Cadastros.Produto.Produto> GetByNameAsync(string nome, int idEmpresa)
    {
        var produto = await _contextBase.Produto
            .FirstOrDefaultAsync(p => p.Nome == nome && (p.IdEmpresa == idEmpresa));
        return produto;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetAllAsync(string? nomeProduto, int idEmpresa)
    {
        var query = _contextBase.Produto.AsQueryable();

        // Aplica o filtro no nome apenas se o parâmetro nomeProduto não for vazio
        if (!string.IsNullOrEmpty(nomeProduto))
        {
            query = query.Where(p => p.Nome.Contains(nomeProduto));
        }

        query = query.Where(p => p.IdEmpresa == idEmpresa);

        var entities = await query.ToListAsync();
        return entities;
    }


    public async Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
    {
        var entities = await _contextBase.Produto
            .Where(ab => (ab.IdEmpresa == idEmpresa) && 
                          ab.DataSituacao > dataUltimaSincronizacao)
            .ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Produto.Produto> GetByIdAsync(int? id, int idEmpresa)
    {
        var obj = await _contextBase.Produto
            .Where(x => x.Id == id &&
                        (x.IdEmpresa == idEmpresa))
            .FirstOrDefaultAsync();
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Produto.Produto obj)
    {
        var objeto = await _contextBase.Produto.FindAsync(obj.Id);
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.IdTipoDeFormulacao = obj.IdTipoDeFormulacao;
        objeto.IdTipoDeServico = obj.IdTipoDeServico;
        objeto.Classe = obj.Classe;
        objeto.Nome = obj.Nome;
        _contextBase.Produto.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
