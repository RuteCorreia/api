using Dapper;
using Domain.Interfaces.Cadastros.DadosResponsavel;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.DadosResponsavel;

public class DadosResponsavelRepository : IDadosResponsavelRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public DadosResponsavelRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;   
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if (entityToRemove is not null)
        {
            _contextBase.DadosResponsavel.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.DadosResponsavel
             .AsNoTracking()
             .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
             .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel> GetByIdAsync(int id, int idEmpresa)
    {
        using (var connection = _dbConnection)
        {
            try
            {
                string query = @"
                    SELECT * 
                    FROM DadosResponsavel 
                    WHERE Id = @Id 
                    AND (@IdEmpresa = 0 AND IdEmpresa IS NULL OR IdEmpresa = @IdEmpresa)";

                var parameters = new { Id = id, IdEmpresa = idEmpresa };
                var dadosResponsavel = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel>(query, parameters);
                return dadosResponsavel;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.DadosResponsavel.DadosResponsavel obj)
    {
        var objeto = await _contextBase.DadosResponsavel.FindAsync(obj.Id);
        objeto.Data = obj.Data;
        objeto.UF = obj.UF;
        objeto.Cidade = obj.Cidade;
        objeto.NomeCompleto = obj.NomeCompleto;
        objeto.Documento = obj.Documento;
        objeto.Telefone = obj.Telefone;
        objeto.assinaturaResponsavel = obj.assinaturaResponsavel;

        _contextBase.DadosResponsavel.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
