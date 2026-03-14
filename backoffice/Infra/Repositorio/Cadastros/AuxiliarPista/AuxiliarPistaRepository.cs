using Dapper;
using Domain.Entidades.Cadastros.AuxiliarPista;
using Domain.Interfaces.Cadastros.AuxiliarPista;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infra.Repositorio.Cadastros.AuxiliarPista
{
    public class AuxiliarPistaRepository : IAuxiliarPistaRepository
    {
        private readonly IDbConnection _dbConnection;
        public AuxiliarPistaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista obj)
        {
            try
            {
                using (var connection = _dbConnection)
                {
                    string sqlQuery = @"INSERT INTO AuxiliarPista (Nome, Documento, IdEmpresa) VALUES (@Nome, @Documento, @IdEmpresa);
                                SELECT CAST(SCOPE_IDENTITY() as int)"
                    ;
                    int id = await connection.QueryFirstOrDefaultAsync<int>(sqlQuery, new { obj.Nome, obj.Documento, obj.IdEmpresa });

                    obj.Id = id;

                    return id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar AuxiliarPista: " + ex.Message);
            }
        }

        public async Task<Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista?> GetByIdAsync(int id)
        {
            try
            {
                string sqlQuery = "SELECT Id, Nome, Documento, IdEmpresa FROM AuxiliarPista WHERE Id = @Id";
                using (var connection = _dbConnection)
                {
                    var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.AuxiliarPista.AuxiliarPista>(sqlQuery, new { Id = id });
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter AuxiliarPista: " + ex.Message);
            }
        }
    }
}
