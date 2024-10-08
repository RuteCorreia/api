using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.Produto;
using Domain.Interfaces.Cadastros.Bula;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Bula;

public class BulaRepository : IBulaRepository
{
    private readonly ContextBase _contextBase;

    public BulaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemoveOrDeactivate = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemoveOrDeactivate))
        {
            var hasFk = await _contextBase.BulaAplicacao
                .AnyAsync(x => x.IdBula == entityToRemoveOrDeactivate.IdBula);
                        
            if (hasFk)
            {
                entityToRemoveOrDeactivate.Removido = true;
                _contextBase.Bula.Update(entityToRemoveOrDeactivate);
            }
            else
            {
                _contextBase.Bula.Remove(entityToRemoveOrDeactivate);
            }

            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Bula>> GetAllAsync(int idEmpresa)
    {
        var query = @"SELECT * FROM Bula WHERE Removido = 0 AND IdEmpresa IN (@IdEmpresa, 196)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Empresa.Bula>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id)
    {
        var obj = await _contextBase.Bula.FirstOrDefaultAsync(x => !x.Removido && x.IdProduto == id);
        return obj;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Bula>> GetByIdProdutoAsync(int idProduto, int idEmpresa)
    {

        var query = @"SELECT * FROM Bula WHERE IdProduto = @IdProduto AND Removido = 0 AND IdEmpresa IN (@IdEmpresa, 196)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdProduto = idProduto, IdEmpresa = idEmpresa };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Empresa.Bula>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name, int idEmpresa)
    {
        var idEmpresaAdministracao = 21;
        var obj = await _contextBase.Bula
            .FirstOrDefaultAsync(x => x.NomeProduto == name && (x.IdEmpresa == idEmpresa || x.IdEmpresa == idEmpresaAdministracao) && !x.Removido);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Empresa.Bula obj)
    {
        var objeto = await _contextBase.Bula.FindAsync(obj.IdBula);
        objeto.IdCultura = obj.IdCultura;
        objeto.IdAlvoBiologico = obj.IdAlvoBiologico;
        objeto.DoseProdutoComercial = obj.DoseProdutoComercial;
        objeto.IdTipoDeUnidade = obj.IdTipoDeUnidade;
        _contextBase.Bula.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task RemoveRecomendacaoAsync(int idBula)
    {
        var objeto = await _contextBase.Bula.FindAsync(idBula);
        objeto.Removido = true;
        _contextBase.Bula.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task RemoveBulaAsync(int idProduto, int idEmpresa)
    {
        var bulas = await _contextBase.Bula
            .Where(b => b.IdProduto == idProduto && b.IdEmpresa == idEmpresa)
            .ToListAsync();

        if (bulas.Any())
        {
            foreach (var bula in bulas)
            {
                bula.Removido = true;
            }

            _contextBase.Bula.UpdateRange(bulas);
            await _contextBase.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<int>> GetDistinctBulaAsync(int idEmpresa, string? nomeProduto)
    {
        if (nomeProduto == null)
            nomeProduto = "";

        var query = @"SELECT DISTINCT(b.IdProduto) 
                  FROM Bula b 
                  INNER JOIN Produto p ON p.Id = b.IdProduto 
                  WHERE p.Nome LIKE @NomeProduto 
                  AND b.IdEmpresa IN (@IdEmpresa, 196) 
                  AND b.Removido = 0";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            // Define os parâmetros, incluindo o nome do produto com os curingas para o LIKE
            var parameters = new
            {
                IdEmpresa = idEmpresa,
                NomeProduto = $"%{nomeProduto}%" // Adiciona os curingas para o LIKE
            };

            var result = await connection.QueryAsync<int>(query, parameters);
            return result.ToList();
        }
    }
}
