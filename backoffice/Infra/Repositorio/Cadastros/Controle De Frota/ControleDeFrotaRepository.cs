using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.Produto;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Controle_De_Frota;

public class ControleDeFrotaRepository : IControleDeFrotaRepository
{
    private readonly ContextBase _contextBase;

    public ControleDeFrotaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetListByStatusAsync(int idEmpresa, int statusEnvio)
    {
        var query = @"
            SELECT *
            FROM ControleDeFrota WHERE IdEmpresa = @IdEmpresa AND (StatusEnvio =  @statusEnvio OR StatusEnvio = 4)";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio };
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(DateTime? offsetDate, string userName)
    {
        string query = "SELECT * FROM ControleDeFrota WHERE StatusEnvio <> 4";

        if (offsetDate != null)
        {
            query += " AND (CONVERT(VARCHAR, DataAlteracao, 120) > CONVERT(VARCHAR, @offsetDate, 120) " +
                     "OR CONVERT(VARCHAR, DataCriacao, 120) > CONVERT(VARCHAR, @offsetDate, 120))";
        }

        query += " AND (NomeExecutor = @Executor OR NomePiloto = @Piloto)";

        var parameters = new
        {
            offsetDate = offsetDate,
            Executor = userName,
            Piloto = userName
        };

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            // Executa a consulta com os parâmetros
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int? id)
    {
        var query = @"SELECT * FROM ControleDeFrota WHERE Id = @Id";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Id = id };
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result;
        }
    }

    public async Task<int?> UpdateAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        var objeto = await _contextBase.ControleDeFrota.FindAsync(obj.Id);
        if (objeto != null)
        {
            objeto.Observacao = obj.Observacao;
            objeto.Data = obj.Data;
            objeto.IdVeiculo = obj.IdVeiculo;
            objeto.NomeVeiculo = obj.NomeVeiculo;
            objeto.DataCriacao = obj.DataCriacao;
            objeto.DataAtualizacao = obj.DataAtualizacao;
            objeto.KmInicial = obj.KmInicial;
            objeto.KmFinal = obj.KmFinal;
            objeto.IdAeronave = obj.IdAeronave;
            objeto.NomeAeronave = obj.NomeAeronave;
            objeto.HorimetroInicial = obj.HorimetroInicial;
            objeto.HorimetroFinal = obj.HorimetroFinal;
            objeto.CombustivelInicial = obj.CombustivelInicial;
            objeto.CombustivelFinal = obj.CombustivelFinal;
            objeto.QtdeCombustivel = obj.QtdeCombustivel;
            objeto.IdPiloto = obj.IdPiloto;
            objeto.NomePiloto = obj.NomePiloto;
            objeto.IdExecutor = obj.IdExecutor;
            objeto.NomeExecutor = obj.NomeExecutor;
            objeto.Extensao = obj.Extensao;
            objeto.Combustivel = obj.Combustivel;
            objeto.IdData = obj.IdData;
            objeto.Imagem = obj.Imagem;
            objeto.NomeRelatorio = obj.NomeRelatorio;
            objeto.StatusEnvio = obj.StatusEnvio;
            objeto.IsDrone = obj.IsDrone;
            objeto.Checklist = obj.Checklist;

            _contextBase.ControleDeFrota.Update(objeto);
            await _contextBase.SaveChangesAsync(); 
            return objeto.Id;
        }
        return null;
    }
}
