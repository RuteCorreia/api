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
        public async Task<IEnumerable<RelatorioAeronave>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    var query = @"SELECT 
                        ar.NomeAeronave,
                        SUM(CAST(REPLACE(REPLACE(LTRIM(RTRIM(iat.Extensao)), ',', '.'), '.', '') AS DECIMAL(18, 2))) AS ExtensaoTotal,
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
                    WHERE 
                        ra.IdEmpresa = 196
                        AND ra.StatusEnvio = 0
                    GROUP BY 
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
    }
}
