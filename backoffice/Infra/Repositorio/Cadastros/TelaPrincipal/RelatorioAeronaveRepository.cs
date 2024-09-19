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
        public async Task<IEnumerable<RelatorioAeronave>> GetAllAplicacaoAsync()
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"SELECT 
						ra.Piloto,
						ra.Executor,
                        LEFT(ar.NomeAeronave, 
        CASE 
            WHEN CHARINDEX(' - ', ar.NomeAeronave) > 0 
            THEN CHARINDEX(' - ', ar.NomeAeronave) - 1
            ELSE LEN(ar.NomeAeronave)
        END
    ) AS Aeronave,
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
						AplicacaoRelatorio are on ra.AplicacaoRelatorioId = are.Id
                    WHERE 
                        ra.IdEmpresa = 196
                        AND ra.StatusEnvio = 0
                    GROUP BY 
						ra.Piloto,
						ra.Executor,
                        ar.NomeAeronave
                    ORDER BY 
                        ar.NomeAeronave;";

                    var result = await connection.QueryAsync<RelatorioAeronave>(query);
                    return result.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<IEnumerable<RelatorioAeronave>> GetAllIncendioAsync()
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"SELECT 
						ci.Piloto,
						usu.Nome as Executor,
                        aer.Prefixo as Aeronave,
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
                        ci.IdEmpresa = 196
                        AND ci.StatusEnvio = 0
                    GROUP BY 
						ci.Piloto,
						usu.Nome,
                        aer.Prefixo
                    ORDER BY 
                        aer.Prefixo;";

                    var result = await connection.QueryAsync<RelatorioAeronave>(query);
                    return result.ToList();
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
