using Dapper;
using Domain.Entidades.Cadastros.TelaPrincipal;
using Domain.Interfaces.Cadastros.Dashboard;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;

namespace Infra.Repositorio.Cadastros.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ContextBase _contextBase;
        public DashboardRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            DATEPART(YEAR, ra.DataCriacao) AS Ano,
                            DATEPART(MONTH, ra.DataCriacao) AS Mes,
                            SUM(CAST(REPLACE(LTRIM(RTRIM(are.TotalAreaAplicada)), ',', '.') AS DECIMAL(18, 2))) AS ExtensaoTotal,
                            SUM(CAST(REPLACE(REPLACE(REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), '.', ''), ',', '.') AS DECIMAL(18, 2))) AS ValorTotal
                        FROM 
                            RelatorioAplicacao ra
                        JOIN 
                            ContratoPrestacaoServico cps ON ra.ContratoPrestacaoServicoId = cps.Id
                        JOIN 
                            AplicacaoRecomendacoesTecnicas ar ON ra.RecomendacoesTecnicasId = ar.Id
                        JOIN
                            IdentificacaoAreaTratada iat ON ra.IdentificacaoAreaTratadaId = iat.Id
                        JOIN 
                            AplicacaoRelatorioItem rli ON ra.AplicacaoRelatorioId = rli.IdAplicacaoRelatorio
                        JOIN
                            AplicacaoRelatorio are ON ra.AplicacaoRelatorioId = are.Id
                        JOIN
                            Contratante c ON ra.ContratanteId = c.Id
                        WHERE
                            ra.IdEmpresa = @IdEmpresa
                            AND ra.StatusEnvio = 0
                            AND (ra.DataCriacao >= @DataInicio OR @DataInicio IS NULL)
                            AND (ra.DataCriacao <= @DataFim OR @DataFim IS NULL)
                            AND (@Usuario IS NULL OR ra.Piloto LIKE '%' + @Usuario + '%' OR ra.Executor LIKE '%' + @Usuario + '%')
                            AND (ar.NomeAeronave LIKE '%' + @NomeAeronave + '%' OR @NomeAeronave IS NULL)
                            AND (c.Nome LIKE '%' + @NomeContratante + '%' OR @NomeContratante IS NULL)
                        GROUP BY 
                            DATEPART(YEAR, ra.DataCriacao), 
                            DATEPART(MONTH, ra.DataCriacao)
                        ORDER BY 
                            Ano, Mes;";

                    var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Dashboard.Dashboard>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            DATEPART(YEAR, ci.DataCriacao) AS Ano,
                            DATEPART(MONTH, ci.DataCriacao) AS Mes,
                            SUM(
                               CAST(
                                    REPLACE(
                                        REPLACE(
                                            REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), 
                                            '.', ''), 
                                        ',', '.')   
                                    AS DECIMAL(18, 2)
                                )
                            ) AS ValorTotal
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
                            AND (@Usuario IS NULL OR ci.Piloto LIKE '%' + @Usuario + '%' OR usu.Nome LIKE '%' + @Usuario + '%')
                            AND (@NomeAeronave IS NULL OR aer.Prefixo LIKE '%' + @NomeAeronave + '%')
                            AND (@NomeContratante IS NULL OR ci.Cliente LIKE '%' + @NomeContratante + '%')
                        GROUP BY 
                            DATEPART(YEAR, ci.DataCriacao), 
                            DATEPART(MONTH, ci.DataCriacao)
                        ORDER BY 
                            Ano, Mes;";

                    var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Dashboard.Dashboard>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAllFrotaAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            DATEPART(YEAR, cf.DataCriacao) AS Ano,
                            DATEPART(MONTH, cf.DataCriacao) AS Mes,
                            SUM(TRY_CAST(NULLIF(cf.HorimetroFinal, '') AS DECIMAL(18, 2)) - 
                                TRY_CAST(NULLIF(cf.HorimetroInicial, '') AS DECIMAL(18, 2))) AS TotalHoras
                        FROM 
                            ControleDeFrota cf
                        WHERE
                            cf.IdEmpresa = @IdEmpresa
                            AND cf.StatusEnvio = 0
                            AND (cf.DataCriacao >= @DataInicio OR @DataInicio IS NULL)
                            AND (cf.DataCriacao <= @DataFim OR @DataFim IS NULL)
                            AND (@Usuario IS NULL OR cf.NomePiloto LIKE '%' + @Usuario + '%' OR cf.NomeExecutor LIKE '%' + @Usuario + '%')
                            AND (cf.NomeAeronave LIKE '%' + @NomeAeronave + '%' OR @NomeAeronave IS NULL)
                        GROUP BY 
                            DATEPART(YEAR, cf.DataCriacao), 
                            DATEPART(MONTH, cf.DataCriacao)
                        ORDER BY 
                            Ano, Mes;";

                    var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Dashboard.Dashboard>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<string>> GetUsuariosDropdownAsync(int idEmpresa)
        {
            var query = @"
                SELECT DISTINCT Piloto AS Nome
                FROM RelatorioAplicacao
                WHERE StatusEnvio = 0 AND IdEmpresa = @IdEmpresa
        
                UNION
        
                SELECT DISTINCT Executor AS Nome
                FROM RelatorioAplicacao
                WHERE StatusEnvio = 0 AND IdEmpresa = @IdEmpresa
        
                UNION
        
                SELECT DISTINCT Piloto AS Nome
                FROM CombateIncendio
                WHERE StatusEnvio = 0 AND IdEmpresa = @IdEmpresa
        
                UNION
        
                SELECT DISTINCT u.Nome AS Nome
                FROM CombateIncendio ci
                JOIN Usuario u ON ci.IdExecutor = u.Id 
                WHERE ci.StatusEnvio = 0 AND ci.IdEmpresa = @IdEmpresa;
            ";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var usuarios = await connection.QueryAsync<string>(query, new { IdEmpresa = idEmpresa });
                return usuarios.ToList();
            }
        }

        public async Task<IEnumerable<string>> GetClientesDropdownAsync(int idEmpresa)
        {
            var query = @"
                SELECT DISTINCT c.Nome AS Nome
                FROM RelatorioAplicacao ra
                JOIN Contratante c ON ra.ContratanteId = c.Id
                WHERE ra.StatusEnvio = 0 AND ra.IdEmpresa = @IdEmpresa
        
                UNION
        
                SELECT DISTINCT Cliente AS Nome
                FROM CombateIncendio
                WHERE StatusEnvio = 0 AND IdEmpresa = @IdEmpresa;
            ";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var usuarios = await connection.QueryAsync<string>(query, new { IdEmpresa = idEmpresa });
                return usuarios.ToList();
            }
        }

        public async Task<IEnumerable<string>> GetAeronavesDropdownAsync(int idEmpresa)
        {
            var query = @"
                SELECT DISTINCT 
                    LEFT(art.NomeAeronave, CHARINDEX(' ', art.NomeAeronave + ' ') - 1) AS Aeronave
                FROM RelatorioAplicacao ra
                JOIN AplicacaoRecomendacoesTecnicas art ON ra.RecomendacoesTecnicasId = art.Id
                WHERE ra.StatusEnvio = 0 AND ra.IdEmpresa = @IdEmpresa
        
                UNION
        
                SELECT DISTINCT Prefixo AS Aeronave
                FROM CombateIncendio c
                JOIN Aeronave a ON c.IdAeronave = a.Id
                WHERE c.StatusEnvio = 0 AND c.IdEmpresa = @IdEmpresa;
            ";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var usuarios = await connection.QueryAsync<string>(query, new { IdEmpresa = idEmpresa });
                return usuarios.ToList();
            }
        }

    }
}
