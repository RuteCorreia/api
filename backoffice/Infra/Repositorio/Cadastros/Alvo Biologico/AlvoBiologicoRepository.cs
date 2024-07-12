using Dapper;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync()
    {
        var entities = await _contextBase.AlvoBiologico.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AlvoBiologico.FindAsync(id);
        return obj;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAlvosBiologicosAsync(string nomeCultura, string nomeProduto)
    {
        var query = @"
            SELECT ab.*
            FROM BulaAplicacao ba
            JOIN Cultura c ON ba.idCultura = c.idCultura
            JOIN AlvoBiologico ab ON ba.idAlvoBiologico = ab.Id
            JOIN Produto p ON ab.IdProduto = p.Id
            WHERE c.Nome = @NomeCultura
            AND p.Nome = @NomeProduto";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { NomeCultura = nomeCultura, NomeProduto = nomeProduto };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetByIdCulturaAsync(int id)
    {
        var entities = await _contextBase.BulaAplicacao.Where(w => w.IdCultura == id).GroupBy(g => g.IdAlvoBiologico).Select(g => g.FirstOrDefault().AlvoBiologico).ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByNameAsync(string name)
    {
        var obj = _contextBase.AlvoBiologico.Where(x => x.Nome == name).FirstOrDefault();
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
