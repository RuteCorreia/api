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

        public async Task AddAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
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
            string query = "SELECT * FROM RelatorioAplicacao WHERE CAST(Data AS DATE) = @Date";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>(query, new { Date = date.Date });
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioAplicacao.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj)
        {
            var objeto = await _contextBase.RelatorioAplicacao.FindAsync(obj.Id);

            _contextBase.RelatorioAplicacao.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
