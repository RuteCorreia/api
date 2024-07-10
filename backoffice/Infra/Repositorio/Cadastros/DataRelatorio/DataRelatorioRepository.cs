using Dapper;
using Domain.Interfaces.Cadastros.DataRelatorio;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.DataRelatorio
{
    public class DataRelatorioRepository : IDataRelatorioRepository
    {
        private readonly ContextBase _contextBase;
        private readonly string _connectionString;

        public DataRelatorioRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
            _connectionString = _contextBase.ObterStringConexao();
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
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

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT * 
                    FROM DataRelatorio 
                    WHERE Id = @Id 
                    AND (@IdEmpresa = 0 AND IdEmpresa IS NULL OR IdEmpresa = @IdEmpresa)";

                    var parameters = new { Id = id, IdEmpresa = idEmpresa };
                    var data = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.DataRelatorio.DataRelatorio>(query, parameters);
                    return data;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //var obj = await _contextBase.DataRelatorio.FirstOrDefaultAsync(x => x.Id == id && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
            //return obj;
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
