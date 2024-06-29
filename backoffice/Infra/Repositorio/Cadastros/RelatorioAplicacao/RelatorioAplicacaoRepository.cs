using Dapper;
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
            using (var connection = _sqlConnection)
            {
                try
                {
                    string query = @"
                        INSERT INTO RelatorioAplicacao 
                        (ContratanteId, IdentificacaoAreaTratadaId, CaracteristicasProdutoAplicadoId, 
                         RecomendacoesTecnicasId, AplicacaoRelatorioId, ContratoPrestacaoServicoId, 
                         DadosResponsavelId, CulturaId, PilotoId, Piloto, 
                         ExecutorId, Executor, AuxiliarPistaId, IsDrone, RefDocument, 
                         DataCriacao, DataAlteracao, Data, RefUsuario, StatusEnvio, IdEmpresa)
                        OUTPUT INSERTED.*
                        VALUES 
                        (@ContratanteId, @IdentificacaoAreaTratadaId, @CaracteristicasProdutoAplicadoId, 
                         @RecomendacoesTecnicasId, @AplicacaoRelatorioId, @ContratoPrestacaoServicoId, 
                         @DadosResponsavelId, @CulturaId, @PilotoId, @Piloto,  
                         @ExecutorId, @Executor, @AuxiliarPistaId, @IsDrone, @RefDocument, 
                         @DataCriacao, @DataAlteracao, @Data, @RefUsuario, @StatusEnvio, @IdEmpresa);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

                    //relatorioAplicacaoViewModel.CulturaId = culturaId;
                    //relatorioAplicacaoViewModel.PilotoId = pilotoId; 
                    //relatorioAplicacaoViewModel.ExecutorId = executorId;
                    //relatorioAplicacaoViewModel.DataCriacao = dataCriacao;
                    //relatorioAplicacaoViewModel.DataAlteracao = dataAlteracao;

                    var relatorioAplicacao = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new
                    {
                        obj.ContratanteId,
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync()
        {
            string query = "SELECT * FROM RelatorioAplicacao";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query);
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllByIdEmpresaAsync(int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { IdEmpresa = idEmpresa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataCriacaoAsync(DateTime dataCriacao,int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE CONVERT(VARCHAR, DataCriacao, 120) = CONVERT(VARCHAR, @DataCriacao, 120) AND IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { DataCriacao = dataCriacao ,IdEmpresa = idEmpresa });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataAlteracaoAsync(DateTime dataAlteracao, int idEmpresa)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE CONVERT(VARCHAR, DataAlteracao, 120) = CONVERT(VARCHAR, @DataAlteracao, 120) AND @IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { DataAlteracao = dataAlteracao , IdEmpresa = idEmpresa });
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id)
        {
            using (var connection = _dbConnection)
            {
                try
                {
                    string query = $"SELECT * FROM RelatorioAplicacao WHERE Id = {id}";
                    var relatorio = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query);
                    return relatorio;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
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

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);

            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
