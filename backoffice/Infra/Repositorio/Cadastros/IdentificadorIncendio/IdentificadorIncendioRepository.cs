using Dapper;
using Domain.Interfaces.Cadastros.IdentificadorIncendio;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;

namespace Infra.Repositorio.Cadastros.IdentificadorIncendio
{
    public class IdentificadorIncendioRepository : IIdentificadorIncendioRepository
    {
        private readonly ContextBase _contextBase;
        public IdentificadorIncendioRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(int idEmpresa)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var insertQuery = @"
                    BEGIN TRANSACTION;

                    BEGIN TRY
                        DECLARE @NovoIdentificador INT;
                        SET @NovoIdentificador = (SELECT Identificador FROM IdentificadorIncendio WHERE IdEmpresa = @IdEmpresa);

                        IF (@NovoIdentificador IS NULL) 
                        BEGIN
                            INSERT INTO IdentificadorIncendio (Identificador, IdEmpresa)
                            VALUES (1, @IdEmpresa);
                            SET @NovoIdentificador = 1;
                        END
                        ELSE
                        BEGIN
                            SET @NovoIdentificador = @NovoIdentificador + 1
                            UPDATE IdentificadorIncendio
                            SET Identificador = @NovoIdentificador
                            WHERE IdEmpresa = @IdEmpresa
                        END

                        COMMIT;

                        SELECT @NovoIdentificador; 
                    END TRY
                    BEGIN CATCH
                        ROLLBACK;
                        THROW;
                    END CATCH;";

                var result = await connection.QuerySingleAsync<int>(insertQuery, new
                {
                    IdEmpresa = idEmpresa
                });

                return result;
            }   
        }
    }
}
