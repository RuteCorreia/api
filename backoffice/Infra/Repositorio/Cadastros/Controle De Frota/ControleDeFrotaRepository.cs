using Dapper;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Produto;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Controle_De_Frota;

public class ControleDeFrotaRepository : IControleDeFrotaRepository
{
    private readonly ContextBase _contextBase;

    public ControleDeFrotaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>> GetAllAsync(int? idEmpresa)
    {
        var query = @"SELECT * FROM ControleDeFrota WHERE IdEmpresa = @IdEmpresa";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { IdEmpresa = idEmpresa};
            var result = await connection.QueryAsync<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>(query, parameters);
            return result.ToList();
        }
    }

    public async Task<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> GetByIdAsync(int id)
    {
        //var obj = await _contextBase.ControleDeFrota.FindAsync(id);
        return null;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota obj)
    {
        //var objeto = await _contextBase.ControleDeFrota.FindAsync(obj.Id);
        //objeto.Observacao = obj.Observacao;
        //objeto.Data = obj.Data;
        //objeto.IdFrota = obj.IdFrota;
        //objeto.IdAeronave = obj.IdAeronave;
        //objeto.KmInicial = obj.KmInicial;
        //objeto.LocalInicial = obj.LocalInicial;
        //objeto.LocalizacaoPistaLat = obj.LocalizacaoPistaLat;
        //objeto.LocalizacaoPistaLon = obj.LocalizacaoPistaLon;
        //objeto.KmFinal = obj.KmFinal;
        //objeto.HorimetroInicial = obj.HorimetroInicial;
        //objeto.HorimetroFinal = obj.HorimetroFinal;
        //objeto.Combustivel = obj.Combustivel;
        //objeto.QtdeCombustivel = obj.QtdeCombustivel;
        //objeto.QtdeHectare = obj.QtdeHectare;
        //objeto.IdPiloto = obj.IdPiloto;

        //_contextBase.ControleDeFrota.Update(objeto);
        //await _contextBase.SaveChangesAsync();
    }
}
