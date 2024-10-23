using Dapper;
using Domain.Interfaces.Cadastros.IdentificacaoAreaTratada;
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

namespace Infra.Repositorio.Cadastros.IdentificacaoAreaTratada
{
    public class IdentificacaoAreaTratadaRepository : IIdentificacaoAreaTratadaRepository
    {
        private readonly ContextBase _contextBase;
        private readonly IDbConnection _dbConnection;

        public IdentificacaoAreaTratadaRepository(ContextBase contextBase, IDbConnection dbConnection)
        {
            _contextBase = contextBase;
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAsync(Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj)
        {
            _contextBase.Add(obj);
            _contextBase.SaveChanges();
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>> GetAllAsync()
        {
            var entities = await _contextBase.IdentificacaoAreaTratada.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada> GetByIdAsync(int? id)
        {
            using (var connection = _dbConnection)
            {
                try
                {
                    string query = $"SELECT * FROM IdentificacaoAreaTratada WHERE Id = {id}";
                    var areaTratada = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(query);
                    return areaTratada;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada> GetForExportExcelAsync(int? id)
        {
            var query = @"
            SELECT UF,Cidade,Cultura
            FROM IdentificacaoAreaTratada WHERE Id = @Id";

            using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
            {
                var parameters = new { Id = id };
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>(query, parameters);
                return result;
            }
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj)
        {
            var objeto = await _contextBase.IdentificacaoAreaTratada.FindAsync(obj.Id);
            objeto.UF = obj.UF;
            objeto.Localizacao = obj.Localizacao;
            objeto.Cultura = obj.Cultura;
            objeto.Extensao = obj.Extensao;
            objeto.Cidade = obj.Cidade;
            objeto.CroquiArea = obj.CroquiArea;
            objeto.Marcadores = obj.Marcadores;

            _contextBase.IdentificacaoAreaTratada.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
