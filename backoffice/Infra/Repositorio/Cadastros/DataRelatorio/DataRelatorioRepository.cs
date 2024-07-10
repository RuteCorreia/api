using Dapper;
using Domain.Interfaces.Cadastros.DataRelatorio;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.DataRelatorio
{
    public class DataRelatorioRepository : IDataRelatorioRepository
    {
        private readonly ContextBase _contextBase;
        private readonly IDbConnection _dbConnection;

        public DataRelatorioRepository(ContextBase contextBase, IDbConnection dbConnection)
        {
            _contextBase = contextBase;
            _dbConnection = dbConnection;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
            return obj.Id;
        }

        public async Task DeleteAsync(int id, int idEmpresa)
        {
            var entityToRemove = await GetByIdAsync(id, idEmpresa);
            if (entityToRemove is not null)
            {
                _contextBase.DataRelatorio.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>> GetAllAsync(int idEmpresa)
        {
            var entities = await _contextBase.DataRelatorio
             .AsNoTracking()
             .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
             .ToListAsync();

            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio> GetByIdAsync(int? id, int idEmpresa)
        {
            using (var connection = _dbConnection)
            {
                try
                {
                    string query = @"
                    SELECT * 
                    FROM DataRelatorio 
                    WHERE Id = @Id 
                    AND (@IdEmpresa = 0 AND IdEmpresa IS NULL OR IdEmpresa = @IdEmpresa)";

                    var parameters = new { Id = id, IdEmpresa = idEmpresa };
                    var dadosResponsavel = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(query, parameters);
                    return dadosResponsavel;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio obj)
        {
            var objeto = await _contextBase.DataRelatorio.FindAsync(obj.Id);
            objeto.Data = obj.Data;
            

            _contextBase.DataRelatorio.Update(objeto);
            await _contextBase.SaveChangesAsync();
            return objeto.Id;
        }
    }
}
