using Dapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.CombateIncendioPista;
using Helpers;
using Infra.Configuracao;
using System.Data;

namespace Infra.Repositorio.Cadastros.CombateIncendioPista
{
    public class CombateIncendioPistaRepository : ICombateIncendioPistaRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ContextBase _contextBase;

        public CombateIncendioPistaRepository(IDbConnection dbConnection, ContextBase contextBase)
        {
            _dbConnection = dbConnection;
            _contextBase = contextBase;
        }
        public async Task<int> AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
            return obj.Id;
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>> GetByCombateIncendioIdAsync(int combateIncendioId)
        {
            string query = "SELECT * FROM CombateIncendioPista WHERE CombateIncendioId = @CombateIncendioId";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>(query, new { CombateIncendioId = combateIncendioId });
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>> GetAllAsync(int? idEmpresa)
        {
            string query = "SELECT * FROM CombateIncendioPista WHERE IdEmpresa = @IdEmpresa";
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista>(query, new { IdEmpresa = idEmpresa });
        }

        public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista> GetByIdAsync(int id)
        {
            var obj = await _contextBase.CombateIncendioPista.FindAsync(id);
            return obj;
        }

        public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioPista obj)
        {
            var objeto = await _contextBase.CombateIncendioPista.FindAsync(obj.Id);
            objeto.HorarioChegadaPista = obj.HorarioChegadaPista;
            objeto.HorimetroChegadaPista = obj.HorimetroChegadaPista;
            objeto.CodigoICAOPista = obj.CodigoICAOPista;
            objeto.NomePista = obj.NomePista;
            objeto.LatPista = obj.LatPista;
            objeto.LongPista = obj.LongPista;
            objeto.IdEmpresa = obj.IdEmpresa;
            objeto.CombateIncendioId = obj.CombateIncendioId;

            _contextBase.CombateIncendioPista.Update(objeto);
            await _contextBase.SaveChangesAsync();
            return objeto.Id;
        }
    }
}
