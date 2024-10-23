using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Produto;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AlvoBiologico;

public class AlvoBiologicoRepository : IAlvoBiologicoRepository
{
    private readonly ContextBase _contextBase;

    public AlvoBiologicoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj)
    {
        await _contextBase.AddAsync(obj);
        try
        {
            await _contextBase.SaveChangesAsync();
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Erro ao atualizar o banco de dados: {dbEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }

    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if (!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.AlvoBiologico
                                    .Where(ab => ab.IdEmpresa == 196 || ab.IdEmpresa == idEmpresa)
                                    .OrderBy(ab => ab.Nome)
                                    .ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int? id)
    {
        var obj = await _contextBase.AlvoBiologico.FindAsync(id);
        return obj;
    }


    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAlvosBiologicosAsync(int idCultura, int idProduto)
    {
        var query = @"SELECT * FROM AlvoBiologico WHERE IdCultura = @IdCultura AND IdProduto = @IdProduto";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdCultura = idCultura, IdProduto = idProduto  };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetByIdCulturaAsync(int id)
    {
        var entities = await _contextBase.BulaAplicacao.Where(w => w.IdCultura == id).GroupBy(g => g.IdAlvoBiologico).Select(g => g.FirstOrDefault().AlvoBiologico).ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByNameAsync(string name, int? idEmpresa)
    {
        var obj = _contextBase.AlvoBiologico.Where(x => x.Nome == name && (x.IdEmpresa == idEmpresa && x.IdEmpresa == 196)).FirstOrDefault();
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj)
    {
        var objeto = await _contextBase.AlvoBiologico.FindAsync(obj.Id);
        objeto.IdProduto = obj.IdProduto;
        objeto.Nome = obj.Nome;
        objeto.DoseProdutoPorHectare = obj.DoseProdutoPorHectare;

        _contextBase.AlvoBiologico.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
