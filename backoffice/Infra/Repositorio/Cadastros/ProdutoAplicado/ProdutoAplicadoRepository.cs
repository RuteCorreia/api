using Dapper;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.ProdutoAplicado;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.ProdutoAplicado;

public class ProdutoAplicadoRepository : IProdutoAplicadoRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public ProdutoAplicadoRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado obj)
    {
        _contextBase.Add(obj);
        _contextBase.SaveChanges();
        return obj.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if (entityToRemove is not null)
        {
            _contextBase.ProdutoAplicado.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado>> GetAllByIdCaracteristicaProdutoAplicadoAsync(int idCaracteristiacaProdutoAplicado)
    {
        var entities = await _contextBase.ProdutoAplicado
        .AsNoTracking()
        .Where(x => x.IdCaracteristicasProdutoAplicado == idCaracteristiacaProdutoAplicado)
        .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado> GetByIdAsync(int id)
    {   
        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            try
            {
                string query = @"
                    SELECT * 
                    FROM ProdutoAplicado 
                    WHERE Id = @Id";

                var parameters = new { Id = id };
                var produtoAplicado = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado>(query,parameters);
                return produtoAplicado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado obj)
    {
        var objeto = await _contextBase.ProdutoAplicado.FindAsync(obj.Id);
        objeto.NomeProduto = obj.NomeProduto;
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.Classe = obj.Classe;
        objeto.AlvoBiologico = obj.AlvoBiologico;
        objeto.DoseProdutoHectare = obj.DoseProdutoHectare;
        objeto.UnidadeDoseProdutoHectare = obj.UnidadeDoseProdutoHectare;
        objeto.TipoServico = obj.TipoServico;

        _contextBase.ProdutoAplicado.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
