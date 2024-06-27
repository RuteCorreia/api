using Dapper;
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
        private readonly ContextBase _contextBase;
        private readonly IDbConnection _dbConnection;

        public RelatorioAplicacaoRepository(ContextBase contextBase, IDbConnection dbConnection)
        {
            _contextBase = contextBase;
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            try
            {
                string query = @"
            INSERT INTO RelatorioAplicacao 
            (ContratanteId, IdentificacaoAreaTratadaId, CaracteristicasProdutoAplicadoId, 
             RecomendacoesTecnicasId, RelatorioAplicacaoId, ContratoPrestacaoServicoId, 
             DadosResponsavelId, Piloto, Executor, RefDocument, Data, DataCriacao, RefUsuario)
            VALUES 
            (@ContratanteId, @IdentificacaoAreaTratadaId, @CaracteristicasProdutoAplicadoId, 
             @RecomendacoesTecnicasId, @RelatorioAplicacaoId, @ContratoPrestacaoServicoId, 
             @DadosResponsavelId, @Piloto, @Executor, @RefDocument, @Data, @DataCriacao, @RefUsuario);
            SELECT CAST(SCOPE_IDENTITY() as int);
";
                using (var connection = _dbConnection)
                {
                    try
                    {
                        var id = await connection.QueryFirstOrDefaultAsync<int>(query, new
                        {
                            obj.ContratanteId,
                            obj.IdentificacaoAreaTratadaId,
                            obj.CaracteristicasProdutoAplicadoId,
                            obj.RecomendacoesTecnicasId,
                            obj.RelatorioAplicacaoId,
                            obj.ContratoPrestacaoServicoId,
                            obj.DadosResponsavelId,
                            obj.Piloto,
                            obj.Executor,
                            obj.RefDocument,
                            obj.Data,
                            //obj.DataCriacao,
                            obj.RefUsuario
                        });
                        obj.Id = id;
                        return id;
                    }catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDateAsync(DateTime date)
        {
            string query = "SELECT * FROM RelatorioAplicacao WHERE DateCriacao = @Date";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { Date = date.Date });
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

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);

            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
