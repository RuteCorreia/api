using Dapper;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.Cadastros.IdentificadorAplicacao;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infra.Repositorio.Cadastros.IdentificadorAplicacao
{
    public class IdentificadorAplicacaoRepository : IIdentificadorAplicacaoRepository
    {
        private readonly ContextBase _contextBase;
        public IdentificadorAplicacaoRepository(ContextBase contextBase)
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
                        SET @NovoIdentificador = (SELECT Identificador FROM IdentificadorAplicacao WHERE IdEmpresa = @IdEmpresa);

                        IF (@NovoIdentificador IS NULL) 
                        BEGIN
                            INSERT INTO IdentificadorAplicacao (Identificador, IdEmpresa)
                            VALUES (1, @IdEmpresa);
                        END
                        ELSE
                        BEGIN
                            SET @NovoIdentificador = @NovoIdentificador + 1
                            UPDATE IdentificadorAplicacao
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
