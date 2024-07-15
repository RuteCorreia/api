using Dapper;
using Domain.Interfaces.Cadastros.CaracteristicasProdutoAplicado;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infra.Repositorio.Cadastros.CaracteristicasProdutoAplicado;

public class CaracteristicasProdutoAplicadoRepository : ICaracteristicasProdutoAplicadoRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public CaracteristicasProdutoAplicadoRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;
        _dbConnection = dbConnection;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj)
    {
        _contextBase.Add(obj);
        _contextBase.SaveChanges();
        return obj.Id;
    }

    public async Task DeleteAsync(int id, int idEmpresa)
    {
        var entityToRemove = await GetByIdAsync(id, idEmpresa);
        if (entityToRemove is not null)
        {
            _contextBase.CaracteristicasProdutoAplicado.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>> GetAllAsync(int idEmpresa)
    {
        var entities = await _contextBase.CaracteristicasProdutoAplicado
            .AsNoTracking()
            .Where(x => idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa)
            .ToListAsync();

        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetForExportExcelAsync(int? id)
    {
        var query = @"
            SELECT Cultura, NomeProduto, Classe, TipoServico
            FROM CaracteristicasProdutoAplicado WHERE Id = @Id";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Id = id };
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(query, parameters);
            return result;
        }
    }

    public async Task<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado> GetByIdAsync(int id, int idEmpresa)
    {
        //var obj = await _contextBase.CaracteristicasProdutoAplicado
        //    .FirstOrDefaultAsync(x => x.Id == id && (idEmpresa == 0 ? x.IdEmpresa == null : x.IdEmpresa == idEmpresa));
        //return obj;
        
        using (var connection = _dbConnection)
        {
            try
            {
                string query = @"
                    SELECT * 
                    FROM CaracteristicasProdutoAplicado 
                    WHERE Id = @Id 
                    AND (@IdEmpresa = 0 AND IdEmpresa IS NULL OR IdEmpresa = @IdEmpresa)";

                var parameters = new { Id = id, IdEmpresa = idEmpresa };
                var produtoAplicado = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado>(query,parameters);
                return produtoAplicado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado obj)
    {
        var objeto = await _contextBase.CaracteristicasProdutoAplicado.FindAsync(obj.Id);
        objeto.Cultura = obj.Cultura;
        objeto.ReceiturarioAgronomico = obj.ReceiturarioAgronomico;
        objeto.NomeProduto = obj.NomeProduto;
        objeto.ClassificacaoToxicologica = obj.ClassificacaoToxicologica;
        objeto.Classe = obj.Classe;
        objeto.TipoFormulacao = obj.TipoFormulacao;
        objeto.AlvoBiologico = obj.AlvoBiologico;
        objeto.DoseProdutoHectare = obj.DoseProdutoHectare;
        objeto.UnidadeDoseProdutoHectare = obj.UnidadeDoseProdutoHectare;
        objeto.Adjuvante = obj.Adjuvante;
        objeto.TipoServico = obj.TipoServico;
        objeto.NumeroReceituarioAgronomico = obj.NumeroReceituarioAgronomico;
        objeto.DataEmissao = obj.DataEmissao;

        _contextBase.CaracteristicasProdutoAplicado.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
