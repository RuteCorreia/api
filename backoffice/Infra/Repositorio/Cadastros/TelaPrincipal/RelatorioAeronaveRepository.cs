using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.Produto;
using Domain.Entidades.Cadastros.TelaPrincipal;
using Domain.Interfaces.Cadastros.TelaPrincipal;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;

namespace Infra.Repositorio.Cadastros.TelaPrincipal
{
    public class RelatorioAeronaveRepository : IRelatorioAeronaveRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ContextBase _contextBase;
        public RelatorioAeronaveRepository(ContextBase contextBase)
        {
            _sqlConnection = new SqlConnection(contextBase.ObterStringConexao());
            _contextBase = contextBase;
        }

        public decimal? GetComissaoAsync(string nome, int idEmpresa)
        {
            var query = @"SELECT Comissao FROM Usuario WHERE Nome = @Nome AND IdEmpresa = @IdEmpresa";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var parameters = new { Nome = nome, IdEmpresa = idEmpresa };
                var result = connection.QueryFirstOrDefault<decimal?>(query, parameters);
                if (result == null)
                {
                    result = 0;
                }
                return result;
            }
        }
        public async Task<IEnumerable<RelatorioAeronave>> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            MIN(ra.Piloto) AS Piloto,
                            MIN(ra.Executor) AS Executor,
                            LEFT(ar.NomeAeronave, 
                                CASE 
                                    WHEN CHARINDEX(' - ', ar.NomeAeronave) > 0 
                                    THEN CHARINDEX(' - ', ar.NomeAeronave) - 1
                                    ELSE LEN(ar.NomeAeronave)
                                END
                            ) AS Aeronave,
                            SUM(CAST(REPLACE(LTRIM(RTRIM(are.TotalAreaAplicada)), ',', '.') AS DECIMAL(18, 2))) AS ExtensaoTotal,
                            SUM(CAST(REPLACE(REPLACE(REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), '.', ''), ',', '.') AS DECIMAL(18, 2))) AS ValorTotal,
                            MIN(ra.DataCriacao) AS DataCriacao
                        FROM 
                            RelatorioAplicacao ra
                        JOIN 
                            ContratoPrestacaoServico cps ON ra.ContratoPrestacaoServicoId = cps.Id
                        JOIN 
                            AplicacaoRecomendacoesTecnicas ar ON ra.RecomendacoesTecnicasId = ar.Id
                        JOIN
                            IdentificacaoAreaTratada iat ON ra.IdentificacaoAreaTratadaId = iat.Id
                        JOIN
                            AplicacaoRelatorio are ON ra.AplicacaoRelatorioId = are.Id
                        WHERE 
                            ra.IdEmpresa = @IdEmpresa
                            AND ra.StatusEnvio = 0
                            AND (@DataInicio IS NULL OR ra.DataCriacao >= @DataInicio)
                            AND (@DataFim IS NULL OR ra.DataCriacao <= @DataFim)
                        GROUP BY 
                            ra.Piloto,
                            ra.Executor,
                            ar.NomeAeronave
                        ORDER BY 
                            DataCriacao;
                    ";

                    // Passando o parâmetro dataFiltro para a consulta
                    var result = await connection.QueryAsync<RelatorioAeronave>(query, new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<RelatorioAeronave>> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa)
        {
            try
            {
                var query = @"
                    SELECT 
                        ci.Piloto,
                        usu.Nome AS Executor,
                        aer.Prefixo AS Aeronave,
                        SUM(
                               CAST(
                                    REPLACE(
                                        REPLACE(
                                            REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), 
                                            '.', ''), 
                                        ',', '.')   
                                    AS DECIMAL(18, 2)
                                )
                            ) AS ValorTotal,
	                SUM(CAST(cps.Extensao AS DECIMAL(10, 2))) AS TotalHoras
                    FROM 
                        CombateIncendio ci
                    JOIN 
                        ContratoPrestacaoServico cps ON ci.ContratoPrestacaoServicoId = cps.Id
                    JOIN 
                        Usuario usu ON ci.IdExecutor = usu.Id
                    JOIN
                        Aeronave aer ON ci.IdAeronave = aer.Id
                    WHERE 
                        ci.IdEmpresa = @IdEmpresa
                        AND ci.StatusEnvio = 0
                        AND (@DataInicio IS NULL OR ci.DataCriacao >= @DataInicio)
                        AND (@DataFim IS NULL OR ci.DataCriacao <= @DataFim)
                    GROUP BY 
                        ci.Piloto,
                        usu.Nome,
                        aer.Prefixo
                    ORDER BY 
                        aer.Prefixo;
                ";

                using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
                {
                    var parameters = new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa };
                    var result = await connection.QueryAsync<RelatorioAeronave>(query, parameters);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter relatórios de aeronave.", ex);
            }
        }

        public async Task<IEnumerable<RelatorioAeronave>> GetAllFrotasAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa)
        {
            try
            {
                var query = @"
                    SELECT
                        LEFT(cf.NomeAeronave, 
                            CASE 
                                WHEN CHARINDEX(' - ', cf.NomeAeronave) > 0 
                                THEN CHARINDEX(' - ', cf.NomeAeronave) - 1
                                ELSE LEN(cf.NomeAeronave)
                            END
                        ) AS Aeronave,
                        SUM(TRY_CAST(NULLIF(cf.HorimetroFinal, '') AS DECIMAL(18, 2)) - 
                        TRY_CAST(NULLIF(cf.HorimetroInicial, '') AS DECIMAL(18, 2))) AS TotalHoras,
                        MIN(cf.DataCriacao) AS DataCriacao
                    FROM 
                        ControleDeFrota cf
                    WHERE 
                        cf.IdEmpresa = @IdEmpresa
                        AND cf.StatusEnvio = 0
                        AND (@DataInicio IS NULL OR cf.DataCriacao >= @DataInicio)
                        AND (@DataFim IS NULL OR cf.DataCriacao <= @DataFim)
                    GROUP BY 
                        cf.NomeAeronave
                    ORDER BY 
                        DataCriacao;
                ";

                using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
                {
                    var parameters = new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa };
                    var result = await connection.QueryAsync<RelatorioAeronave>(query, parameters);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter relatórios de aeronave.", ex);
            }
        }
    }
}
