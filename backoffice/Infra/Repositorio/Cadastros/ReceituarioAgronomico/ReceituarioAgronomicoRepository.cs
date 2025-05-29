using Dapper;
using Domain.Interfaces.Cadastros.ReceituarioAgronomico;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.ReceituarioAgronomico;

public class ReceituarioAgronomicoRepository : IReceituarioAgronomicoRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public ReceituarioAgronomicoRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico obj)
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
            _contextBase.ReceituarioAgronomico.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId)
    {
        var entities = await _contextBase.ReceituarioAgronomico
        .AsNoTracking()
        .Where(x => x.RelatorioAplicacaoId == relatorioAplicacaoId)
        .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico> GetByIdAsync(int id)
    {   
        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            try
            {
                string query = @"
                    SELECT * 
                    FROM ReceituarioAgronomico 
                    WHERE Id = @Id";

                var parameters = new { Id = id };
                var receituarioAgronomico = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico>(query,parameters);
                return receituarioAgronomico;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.ReceituarioAgronomico.ReceituarioAgronomico obj)
    {
        var objeto = await _contextBase.ReceituarioAgronomico.FindAsync(obj.Id);
        objeto.NomeArquivo = obj.NomeArquivo;
        objeto.Numero = obj.Numero;
        objeto.DataEmissao = obj.DataEmissao;

        _contextBase.ReceituarioAgronomico.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
