using Dapper;
using Domain.Entidades.Cadastros.Atividade;
using Domain.Entidades.Cadastros.Horimetro;
using Domain.Entidades.Cadastros.TelaPrincipal;
using Domain.Enums;
using Domain.Interfaces.Cadastros.Dashboard;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text.Json;

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
                                TRY_CAST(NULLIF(cf.HorimetroInicial, '') AS DECIMAL(18, 2))) AS TotalHoras,
                            SUM(cf.Extensao) AS ExtensaoTotal
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetAplicacaoForExportAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            ra.RefDocument AS NumeroDocumento,
	                        ra.Piloto AS Piloto,
	                        ra.Executor AS Executor,
	                        c.Nome as Cliente,
	                        CONVERT(DATE, ra.DataCriacao) AS DataCriacao,
	                        LEFT(ar.NomeAeronave, 
		                        CASE 
			                        WHEN CHARINDEX(' - ', ar.NomeAeronave) > 0 
			                        THEN CHARINDEX(' - ', ar.NomeAeronave) - 1
			                        ELSE LEN(ar.NomeAeronave)
		                        END
	                        ) AS Aeronave,
                            DATEPART(YEAR, ra.DataCriacao) AS Ano,
                            DATEPART(MONTH, ra.DataCriacao) AS Mes,
                            CAST(REPLACE(REPLACE(REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), '.', ''), ',', '.') AS DECIMAL(18, 2)) AS ValorTotal,
                        	CONVERT(DECIMAL(18, 2), 
                                    REPLACE(REPLACE(REPLACE(iat.Extensao, 'R$', ''), ' ', ''), ',', '.')
                                ) AS ExtensaoTotal 
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetIncendioForExportAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            CAST(ci.Id AS VARCHAR(50)) AS NumeroDocumento,
                            ci.Id,
	                        ci.Piloto,
	                        usu.Nome AS Executor,
	                        aer.Prefixo AS Aeronave,
	                        ci.Cliente,
	                        CONVERT(DATE, ci.DataCriacao) AS DataCriacao,
                            DATEPART(YEAR, ci.DataCriacao) AS Ano,
                            DATEPART(MONTH, ci.DataCriacao) AS Mes,
                               CAST(
                                    REPLACE(
                                        REPLACE(
                                            REPLACE(REPLACE(cps.ValorTotal, 'R$', ''), ' ', ''), 
                                            '.', ''), 
                                        ',', '.')   
                                    AS DECIMAL(18, 2)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.Dashboard.Dashboard>> GetFrotaForExportAsync(DateTime? dataInicio, DateTime? dataFim, int idEmpresa, string? usuario, string? nomeAeronave, string? nomeContratante)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"
                        SELECT 
                            CAST(cf.Id AS VARCHAR(50)) AS NumeroDocumento,
                            DATEPART(YEAR, cf.DataCriacao) AS Ano,
                            DATEPART(MONTH, cf.DataCriacao) AS Mes,
	                        cf.NomePiloto AS Piloto,
	                        cf.NomeExecutor AS Executor,
	                        CONVERT(DATE, cf.DataCriacao) AS DataCriacao,
	                        LEFT(cf.NomeAeronave, 
		                        CASE 
			                        WHEN CHARINDEX(' - ', cf.NomeAeronave) > 0 
			                        THEN CHARINDEX(' - ', cf.NomeAeronave) - 1
			                        ELSE LEN(cf.NomeAeronave)
		                        END
	                        ) AS Aeronave,
                            TRY_CAST(NULLIF(cf.HorimetroFinal, '') AS DECIMAL(18, 2)) - 
                                TRY_CAST(NULLIF(cf.HorimetroInicial, '') AS DECIMAL(18, 2)) AS TotalHoras,
                            cf.Extensao AS ExtensaoTotal,
                            CASE WHEN cf.Horimetros = 'null' THEN NULL ELSE cf.Horimetros END Horimetros,
                            IsDrone,
                            HorasAplicacao
                        FROM 
                            ControleDeFrota cf
                        WHERE
                            cf.IdEmpresa = @IdEmpresa
                            AND cf.StatusEnvio = 0
                            AND (cf.DataCriacao >= @DataInicio OR @DataInicio IS NULL)
                            AND (cf.DataCriacao <= @DataFim OR @DataFim IS NULL)
                            AND (@Usuario IS NULL OR cf.NomePiloto LIKE '%' + @Usuario + '%' OR cf.NomeExecutor LIKE '%' + @Usuario + '%')
                            AND (cf.NomeAeronave LIKE '%' + @NomeAeronave + '%' OR @NomeAeronave IS NULL)
                        ORDER BY 
                            Ano, Mes;";

                    var result = await connection.QueryAsync<dynamic>(query,
                        new { DataInicio = dataInicio, DataFim = dataFim, IdEmpresa = idEmpresa, Usuario = usuario, NomeAeronave = nomeAeronave, NomeContratante = nomeContratante });

                    List<Domain.Entidades.Cadastros.Dashboard.Dashboard> dashboards = new List<Domain.Entidades.Cadastros.Dashboard.Dashboard>();

                    foreach (var item in result)
                    {
                        double horasAplicacao = 0, horasIncendio = 0, horasTranslado = 0, horasDrone = 0;
                        decimal extensaoAviao = 0, extensaoDrone = 0;
                        List<Horimetro> horimetros;

                        if (item.IsDrone)
                        {
                            extensaoDrone += item.ExtensaoTotal;

                            if (item.HorasAplicacao != null)
                                horasDrone += Convert.ToDouble(item.HorasAplicacao);
                        }

                        if (!item.IsDrone)
                        {
                            extensaoAviao += item.ExtensaoTotal;

                            if (item.Horimetros != null)
                            {
                                horimetros = GetHorimetros(item.Horimetros);
                                foreach (var horimetro in horimetros)
                                {
                                    switch (horimetro.Tipo)
                                    {
                                        case HorimetroTypeEnum.Aplicacao:
                                            horasAplicacao += horimetro.Fim - horimetro.Inicio;
                                            break;
                                        case HorimetroTypeEnum.Incendio:
                                            horasIncendio += horimetro.Fim - horimetro.Inicio;
                                            break;
                                        case HorimetroTypeEnum.Translado:
                                            horasTranslado += horimetro.Fim - horimetro.Inicio;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }

                        dashboards.Add(new Domain.Entidades.Cadastros.Dashboard.Dashboard
                        {
                            NumeroDocumento = item.NumeroDocumento,
                            Ano = item.Ano,
                            Mes = item.Mes,
                            ExtensaoTotal = extensaoAviao,
                            ExtensaoDrone = extensaoDrone,
                            Piloto = item.Piloto,
                            Executor = item.Executor,
                            Aeronave = item.Aeronave,
                            Cliente = item.Cliente,
                            DataCriacao = item.DataCriacao,
                            ValorTotal = item.ValorTotal,
                            TotalHoras = item.TotalHoras,
                            TotalHorasAplicacao = horasAplicacao,
                            TotalHorasIncendio = horasIncendio,
                            TotalHorasTranslado = horasTranslado,
                            TotalHorasDrone = horasDrone,
                            IsDrone = item.IsDrone
                        });
                    }

                    return dashboards;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        private List<Horimetro> GetHorimetros(dynamic horimetrosJson)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            dynamic deserialezedList = JsonSerializer.Deserialize<List<dynamic>>(horimetrosJson, options);

            List<Horimetro> result = new List<Horimetro>();

            foreach (var item in deserialezedList)
            {
                double inicio = double.Parse(item.GetProperty("inicio").GetString(), new CultureInfo("pt-BR"));
                double fim = double.Parse(item.GetProperty("fim").GetString(), new CultureInfo("pt-BR"));
                int tipoInt = item.GetProperty("tipo").GetInt32();
                HorimetroTypeEnum tipoEnum = (HorimetroTypeEnum)tipoInt;

                result.Add(new Horimetro
                {
                    Inicio = inicio,
                    Fim = fim,
                    Tipo = tipoEnum,
                });
            }

            return result;
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
