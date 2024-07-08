using Dapper;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infra.Repositorio.Cadastros.CombateIncendioDecolagemPouso;

public class CombateIncendioDecolagemPousoRepository : ICombateIncendioDecolagemPousoRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ContextBase _contextBase;

    public CombateIncendioDecolagemPousoRepository(IDbConnection dbConnection, ContextBase contextBase)
    {
        _dbConnection = dbConnection;
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
        return obj.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if(!ObjectNullValidation.IsObjectNull(entityToRemove))
        {
            _contextBase.Remove(entityToRemove);
            await _contextBase.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>> GetAllAsync(int? idEmpresa)
    {
        string query = "SELECT * FROM CombateIncendioDecolagemPouso WHERE IdEmpresa = @IdEmpresa";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(query, new { IdEmpresa = idEmpresa });
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>> GetByCombateIncendioIdAsync(int? combateIncendioId)
    {
        string query = "SELECT * FROM CombateIncendioDecolagemPouso WHERE IdCombateIncendio = @IdCombateIncendio";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>(query, new { IdCombateIncendio = combateIncendioId });
    }

    public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> GetByIdAsync(int id)
    {
        var obj = await _contextBase.CombateIncendioDecolagemPouso.FindAsync(id);
        return obj;
    }

    public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso obj)
    {
        var objeto = await _contextBase.CombateIncendioDecolagemPouso.FindAsync(obj.Id);
        objeto.IdCombateIncendio = obj.IdCombateIncendio;
        objeto.DecolagemHorario = obj.DecolagemHorario;
        objeto.DecolagemHorimetro = obj.DecolagemHorimetro;
        objeto.PousoHorario = obj.PousoHorario;
        objeto.PousoHorimetro = obj.PousoHorimetro;

        _contextBase.CombateIncendioDecolagemPouso.Update(objeto);
        await _contextBase.SaveChangesAsync();
        return objeto.Id;
    }
}
