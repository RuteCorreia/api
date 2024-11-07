using Dapper;
using Domain.Entidades.Cadastros.Atividade;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Domain.Interfaces.Cadastros.RelatorioAplicacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacaoRepository : IRelatorioAplicacaoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ContextBase _contextBase;
        private readonly SqlConnection _sqlConnection;
        public RelatorioAplicacaoRepository(IDbConnection dbConnection, ContextBase contextBase)
        {
            _dbConnection = dbConnection;
            _contextBase = contextBase;
            _sqlConnection = new SqlConnection(contextBase.ObterStringConexao());
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> AddAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                try
                {
                    string query = @"
                        INSERT INTO RelatorioAplicacao 
                        (ContratanteId, NomeRelatorio, IdentificacaoAreaTratadaId, CaracteristicasProdutoAplicadoId, 
                         RecomendacoesTecnicasId, AplicacaoRelatorioId, ContratoPrestacaoServicoId, 
                         DadosResponsavelId, CulturaId, PilotoId, Piloto, 
                         ExecutorId, Executor, AuxiliarPistaId, IsDrone, RefDocument, 
                         DataCriacao, DataAlteracao, Data, IdData, RefUsuario, StatusEnvio, IdEmpresa)
                        OUTPUT INSERTED.*
                        VALUES 
                        (@ContratanteId, @NomeRelatorio, @IdentificacaoAreaTratadaId, @CaracteristicasProdutoAplicadoId, 
                         @RecomendacoesTecnicasId, @AplicacaoRelatorioId, @ContratoPrestacaoServicoId, 
                         @DadosResponsavelId, @CulturaId, @PilotoId, @Piloto,  
                         @ExecutorId, @Executor, @AuxiliarPistaId, @IsDrone, @RefDocument, 
                         @DataCriacao, @DataAlteracao, @Data, @IdData, @RefUsuario, @StatusEnvio, @IdEmpresa);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                    //relatorioAplicacaoViewModel.CulturaId = culturaId;
                    //relatorioAplicacaoViewModel.PilotoId = pilotoId; 
                    //relatorioAplicacaoViewModel.ExecutorId = executorId;
                    //relatorioAplicacaoViewModel.DataCriacao = dataCriacao;
                    //relatorioAplicacaoViewModel.DataAlteracao = dataAlteracao;

                    var relatorioAplicacao = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new
                    {
                        obj.ContratanteId,
                        obj.NomeRelatorio,
                        obj.IdentificacaoAreaTratadaId,
                        obj.CaracteristicasProdutoAplicadoId,
                        obj.RecomendacoesTecnicasId,
                        obj.AplicacaoRelatorioId,
                        obj.ContratoPrestacaoServicoId,
                        obj.DadosResponsavelId,
                        obj.CulturaId,
                        obj.PilotoId,
                        obj.Piloto,
                        obj.ExecutorId,
                        obj.Executor,
                        obj.AuxiliarPistaId,
                        obj.IsDrone,
                        obj.RefDocument,
                        obj.Data,
                        obj.IdData,
                        obj.DataCriacao,
                        obj.DataAlteracao,
                        obj.RefUsuario,
                        obj.StatusEnvio,
                        obj.IdEmpresa
                    });

                    return relatorioAplicacao;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao adicionar relatório de aplicação: " + ex.Message);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Atividade>> GetAtividadesByFiltrosAsync(AtividadeFiltro atividadeFiltro)
        {
            var query = new StringBuilder(@"
            SELECT 
                i.Extensao AS Extensao,
                cps.ValorTotal AS ValorTotalAplicacao,
                (SELECT SUM(TRY_CAST(rli.HorimetroTermino AS DECIMAL(18, 2)) - TRY_CAST(rli.HorimetroInicial AS DECIMAL(18, 2))) 
                 FROM AplicacaoRelatorioItem rli
                 WHERE r.AplicacaoRelatorioId = rli.IdAplicacaoRelatorio) AS TotalHorasAplicacao
            FROM 
                RelatorioAplicacao r
            JOIN 
                IdentificacaoAreaTratada i ON r.IdentificacaoAreaTratadaId = i.Id
            JOIN 
                AplicacaoRecomendacoesTecnicas a ON r.RecomendacoesTecnicasId = a.Id
            JOIN 
                Contratante c ON r.ContratanteId = c.Id
            JOIN 
                ContratoPrestacaoServico cps ON r.ContratoPrestacaoServicoId = cps.Id
            WHERE 
                r.StatusEnvio IN (0, 1)
                AND r.Piloto LIKE '%' + @Piloto + '%'
                AND r.IdEmpresa = @IdEmpresa
                AND r.Executor LIKE '%' + @Executor + '%'
                AND c.Nome LIKE '%' + @Contratante + '%'
                AND a.NomeAeronave LIKE '%' + @PrefixoAeronave + '%'
                AND (@DataInicio IS NULL OR r.DataCriacao >= @DataInicio)
                AND (@DataFim IS NULL OR r.DataCriacao <= @DataFim)");

            var parameters = new DynamicParameters();
            parameters.Add("PrefixoAeronave", atividadeFiltro.PrefixoAeronave);
            parameters.Add("Piloto", atividadeFiltro.Piloto);
            parameters.Add("Executor", atividadeFiltro.Executor);
            parameters.Add("Contratante", atividadeFiltro.Contratante);
            parameters.Add("IdEmpresa", atividadeFiltro.IdEmpresa);
            parameters.Add("DataInicio", atividadeFiltro.DataInicial);
            parameters.Add("DataFim", atividadeFiltro.DataFinal);
            //if (atividadeFiltro.DataInicial != null)
            //{
            //    parameters.Add("DataInicio", atividadeFiltro.DataInicial);
            //}
            //if(atividadeFiltro.DataFinal != null)
            //{
            //    parameters.Add("DataFim", atividadeFiltro.DataFinal);
            //}
            
            

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var result = await connection.QueryAsync<Atividade>(query.ToString(), parameters);
                return result.ToList();
            }
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> ExportExcelAsync(int? id)
        {
            var query = @"
            SELECT IdentificacaoAreaTratadaId, RecomendacoesTecnicasId, CaracteristicasProdutoAplicadoId, AplicacaoRelatorioId
            FROM RelatorioAplicacao WHERE Id = @Id";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var parameters = new { Id = id };
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, parameters);
                return result;
            }
        }


        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync()
        {
            string query = "SELECT * FROM RelatorioAplicacao";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query);
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusAsync(int idEmpresa, int statusEnvio)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa AND (StatusEnvio =  @statusEnvio OR StatusEnvio = 4)  AND IsMapa = 0";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaAsync(int idEmpresa, int statusEnvio)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa AND StatusEnvio =  @statusEnvio  AND IsMapa = 1";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            string query = @"SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa AND StatusEnvio = @statusEnvio AND IsMapa = 1 
                            AND DataCriacao >= @primeiroDiaMes AND DataCriacao <= @ultimoDiaMes";

            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(
                query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio, primeiroDiaMes = primeiroDiaMes, ultimoDiaMes = ultimoDiaMes });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            string query = @"SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa AND (StatusEnvio = @statusEnvio OR StatusEnvio = 4) AND IsMapa = 0 
                            AND DataCriacao >= @primeiroDiaMes AND DataCriacao <= @ultimoDiaMes";

            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(
                query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio, primeiroDiaMes = primeiroDiaMes, ultimoDiaMes = ultimoDiaMes });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllByIdEmpresaAsync(int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { IdEmpresa = idEmpresa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataCriacaoAsync(DateTime dataCriacao, int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE CONVERT(VARCHAR, DataCriacao, 120) > CONVERT(VARCHAR, @DataCriacao, 120) AND IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { DataCriacao = dataCriacao, IdEmpresa = idEmpresa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataAlteracaoAsync(DateTime dataAlteracao, int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE CONVERT(VARCHAR, DataAlteracao, 120) > CONVERT(VARCHAR, @DataAlteracao, 120) AND @IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { DataAlteracao = dataAlteracao, IdEmpresa = idEmpresa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetNovosAsync(DateTime? offsetDate, string userName)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE StatusEnvio <> 4"; // Filtra por StatusEnvio diferente de 4

            if (offsetDate != null)
            {
                query += " AND (CONVERT(VARCHAR, DataAlteracao, 120) > CONVERT(VARCHAR, @offsetDate, 120) " +
                         "OR CONVERT(VARCHAR, DataCriacao, 120) > CONVERT(VARCHAR, @offsetDate, 120))";
            }

            query += " AND (Executor = @Executor OR Piloto = @Piloto)";

            if (offsetDate != null)
            {
                return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { offsetDate = offsetDate, Executor = userName, Piloto = userName });
            }
            else
            {
                return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { Executor = userName, Piloto = userName });
            }

        }


        public async Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id)
        {
            try
            {
                var obj = await _contextBase.RelatorioAplicacao.FindAsync(id);
                return obj;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByIdsAsync(List<int> ids, int idEmpresa, int statusEnvio, int isMapa)
        {

            string query = @"SELECT * FROM RelatorioAplicacao 
                     WHERE Id IN @Ids 
                     AND IdEmpresa = @IdEmpresa 
                     AND (StatusEnvio = @StatusEnvio OR StatusEnvio = 4)
                     AND IsMapa = @IsMapa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { Ids = ids, IdEmpresa = idEmpresa, StatusEnvio = statusEnvio, IsMapa = isMapa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByPilotId(int pilotoId)
        {
            string query = @"
                SELECT *
                FROM DadosResponsavel
                WHERE (@PilotoId = 0 AND PilotoId IS NULL OR PilotoId = @PilotoId)
                ";

            var parameters = new { PilotoId = pilotoId };

            try
            {
                var entities = await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, parameters);
                return entities;
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                Console.WriteLine($"Ocorreu um erro ao buscar os dados responsáveis: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateDataAlteracaoAsync(int? id) 
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(id);

            if (objeto != null)
            {
                // Obtendo a hora local do Brasil
                var brasilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                var dataAlteracao = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, brasilTimeZone);

                objeto.DataAlteracao = dataAlteracao;
                _contextBase.RelatorioAplicacao.Update(objeto);
                await _contextBase.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Relatório com ID {id} não encontrado.");
            }
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);
            objeto.ContratanteId = obj.ContratanteId;
            objeto.IdentificacaoAreaTratadaId = obj.IdentificacaoAreaTratadaId;
            objeto.CaracteristicasProdutoAplicadoId = obj.CaracteristicasProdutoAplicadoId;
            objeto.RecomendacoesTecnicasId = obj.RecomendacoesTecnicasId;
            objeto.AplicacaoRelatorioId = obj.AplicacaoRelatorioId;
            objeto.ContratoPrestacaoServicoId = obj.ContratoPrestacaoServicoId;
            objeto.DadosResponsavelId = obj.DadosResponsavelId;
            objeto.CulturaId = obj.CulturaId;
            objeto.PilotoId = obj.PilotoId;
            objeto.Piloto = obj.Piloto;
            objeto.ExecutorId = obj.ExecutorId;
            objeto.Executor = obj.Executor;
            objeto.AuxiliarPistaId = obj.AuxiliarPistaId;
            objeto.IsDrone = obj.IsDrone;
            objeto.IdData = obj.IdData;
            objeto.DataAlteracao = obj.DataAlteracao;
            objeto.StatusEnvio = obj.StatusEnvio;
            objeto.NomeRelatorio = obj.NomeRelatorio;
            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

        public async Task UpdateIsMapaAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);
            objeto.IsMapa = obj.IsMapa;
            objeto.DataAlteracao = obj.DataAlteracao;
            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

        public async Task CancelarAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);
            objeto.StatusEnvio = obj.StatusEnvio;
            objeto.DataAlteracao = obj.DataAlteracao;
            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }

    }
}
