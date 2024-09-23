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
        public async Task<Domain.Entidades.Cadastros.Dashboard.Dashboard> GetAllAplicacaoAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            DATEPART(YEAR, ra.DataCriacao) AS Ano,
                            DATEPART(MONTH, ra.DataCriacao) AS Mes,
                            SUM(CAST(REPLACE(REPLACE(LTRIM(RTRIM(are.TotalAreaAplicada)), ',', '.'), '.', '') AS DECIMAL(18, 2))) AS ExtensaoTotal,
                            SUM(CAST(REPLACE(REPLACE(REPLACE(cps.ValorTotal, 'R$ ', ''), '.', ''), ',', '.') AS DECIMAL(18, 2))) AS ValorTotal,
                            SUM(DATEDIFF(MINUTE, rli.HoraInicio, rli.HoraTermino) / 60.0) AS TotalHoras
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
                            AND (@DataInicio IS NULL OR ra.DataCriacao >= @DataInicio)
                            AND (@DataFim IS NULL OR ra.DataCriacao <= @DataFim)
                            AND (@Usuario IS NULL OR (ra.Piloto LIKE '%' + @Usuario + '%' OR ra.Executor LIKE '%' + @Usuario + '%'))
                            AND (@NomeAeronave IS NULL OR ar.NomeAeronave LIKE '%' + @NomeAeronave + '%')
                            AND (@NomeContratante IS NULL OR c.Nome LIKE '%' + @NomeContratante + '%')
                        GROUP BY 
                            DATEPART(YEAR, ra.DataCriacao), 
                            DATEPART(MONTH, ra.DataCriacao)
                        ORDER BY 
                            Ano, Mes";

                    var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.Dashboard.Dashboard>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<Domain.Entidades.Cadastros.Dashboard.Dashboard> GetAllIncendioAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            DATEPART(YEAR, ci.DataCriacao) AS Ano,
                            DATEPART(MONTH, ci.DataCriacao) AS Mes,
                            SUM(CAST(REPLACE(REPLACE(REPLACE(cps.ValorTotal, 'R$ ', ''), '.', ''), ',', '.') AS DECIMAL(18, 2))) AS ValorTotal,
                            SUM(DATEDIFF(MINUTE, ci.HoraInicial, ci.HorarioFinalOperacao) / 60.0) AS TotalHoras
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
	                        AND (@Usuario IS NULL OR (ci.Piloto LIKE '%' + @Usuario + '%' OR usu.Nome LIKE '%' + @Usuario + '%'))
	                        AND (@NomeAeronave IS NULL OR aer.Prefixo LIKE '%' + @NomeAeronave + '%')
	                        AND (@NomeContratante IS NULL OR ci.Cliente LIKE '%' + @NomeContratante + '%')
                        GROUP BY 
                            DATEPART(YEAR, ci.DataCriacao), 
                            DATEPART(MONTH, ci.DataCriacao)
                        ORDER BY 
                            Ano, Mes";

                    var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.Dashboard.Dashboard>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

    }
}
