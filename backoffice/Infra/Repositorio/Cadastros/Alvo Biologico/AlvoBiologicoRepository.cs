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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao)
    {
        var empresaRecords = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == idEmpresa
                && ab.DataSituacao > dataUltimaSincronizacao
                && (ab.CampoExcluido == null || ab.CampoExcluido == 0))
            .ToListAsync();

        if (idEmpresa == 196)
            return empresaRecords.OrderBy(ab => ab.Nome).ToList();

        var allOverriddenIds = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == idEmpresa && ab.IdRef != null)
            .Select(ab => ab.IdRef.Value)
            .ToListAsync();
        var overriddenIdsSet = allOverriddenIds.ToHashSet();

        var excludedDefaultIds = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == idEmpresa && ab.IdRef != null && ab.CampoExcluido == 1)
            .Select(ab => ab.IdRef.Value)
            .ToListAsync();
        foreach (var eid in excludedDefaultIds)
            overriddenIdsSet.Add(eid);

        var defaultRecords = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == 196
                && ab.DataSituacao > dataUltimaSincronizacao
                && !overriddenIdsSet.Contains(ab.Id)
                && (ab.CampoExcluido == null || ab.CampoExcluido == 0))
            .ToListAsync();

        return empresaRecords.Concat(defaultRecords).OrderBy(ab => ab.Nome).ToList();
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

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id);
        if (ObjectNullValidation.IsObjectNull(entityToRemove))
            return;

        if (entityToRemove.IdEmpresa == idEmpresa)
        {
            entityToRemove.CampoExcluido = 1;
            _contextBase.AlvoBiologico.Update(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
        else
        {
            var overrideRecord = new Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico
            {
                Nome = entityToRemove.Nome,
                IdProduto = entityToRemove.IdProduto,
                IdCultura = entityToRemove.IdCultura,
                DoseProdutoPorHectare = entityToRemove.DoseProdutoPorHectare,
                IdTipoDeUnidade = entityToRemove.IdTipoDeUnidade,
                IdEmpresa = idEmpresa,
                IdRef = entityToRemove.Id,
                CampoExcluido = 1
            };
            await _contextBase.AddAsync(overrideRecord);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync(int idEmpresa)
    {
        var empresaRecords = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == idEmpresa && (ab.CampoExcluido == null || ab.CampoExcluido == 0))
            .ToListAsync();

        if (idEmpresa == 196)
            return empresaRecords.OrderBy(ab => ab.Nome).ToList();

        var overriddenIds = empresaRecords
            .Where(ab => ab.IdRef != null)
            .Select(ab => ab.IdRef.Value)
            .ToHashSet();

        var excludedDefaultIds = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == idEmpresa && ab.IdRef != null && ab.CampoExcluido == 1)
            .Select(ab => ab.IdRef.Value)
            .ToListAsync();
        foreach (var eid in excludedDefaultIds)
            overriddenIds.Add(eid);

        var defaultRecords = await _contextBase.AlvoBiologico
            .Where(ab => ab.IdEmpresa == 196
                && !overriddenIds.Contains(ab.Id)
                && (ab.CampoExcluido == null || ab.CampoExcluido == 0))
            .ToListAsync();

        return empresaRecords.Concat(defaultRecords).OrderBy(ab => ab.Nome).ToList();
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int? id)
    {
        var obj = await _contextBase.AlvoBiologico.FindAsync(id);
        return obj;
    }


    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAlvosBiologicosAsync(int idCultura, int idProduto, int idEmpresa)
    {
        var query = @"
            SELECT * FROM AlvoBiologico
            WHERE IdCultura = @IdCultura AND IdProduto = @IdProduto AND IdEmpresa = @IdEmpresa
            AND (CampoExcluido IS NULL OR CampoExcluido = 0)
            UNION ALL
            SELECT * FROM AlvoBiologico ab
            WHERE ab.IdCultura = @IdCultura AND ab.IdProduto = @IdProduto AND ab.IdEmpresa = 196
            AND (ab.CampoExcluido IS NULL OR ab.CampoExcluido = 0)
            AND NOT EXISTS (
                SELECT 1 FROM AlvoBiologico o
                WHERE o.IdEmpresa = @IdEmpresa AND o.IdRef = ab.Id
            )
            AND NOT EXISTS (
                SELECT 1 FROM AlvoBiologico o2
                WHERE o2.IdEmpresa = @IdEmpresa AND o2.IdRef = ab.Id AND o2.CampoExcluido = 1
            )
            ORDER BY Nome";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdCultura = idCultura, IdProduto = idProduto, IdEmpresa = idEmpresa };
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
        var obj = _contextBase.AlvoBiologico.Where(x => x.Nome == name && (x.IdEmpresa == idEmpresa || x.IdEmpresa == 196)).FirstOrDefault();
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj, int idEmpresa)
    {
        var objeto = await _contextBase.AlvoBiologico.FindAsync(obj.Id);
        if (objeto == null) return;

        if (objeto.IdEmpresa == idEmpresa)
        {
            objeto.Nome = obj.Nome;
            objeto.IdProduto = obj.IdProduto;
            objeto.DoseProdutoPorHectare = obj.DoseProdutoPorHectare;
            _contextBase.AlvoBiologico.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
        else
        {
            var overrideRecord = new Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico
            {
                IdProduto = obj.IdProduto,
                Nome = obj.Nome,
                DoseProdutoPorHectare = obj.DoseProdutoPorHectare,
                IdCultura = obj.IdCultura,
                IdTipoDeUnidade = obj.IdTipoDeUnidade,
                IdEmpresa = idEmpresa,
                IdRef = objeto.Id
            };
            await _contextBase.AddAsync(overrideRecord);
            await _contextBase.SaveChangesAsync();
        }
    }
}
