using Dapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Infra.Repositorio.Cadastros.AplicacaoRelatorioItem;

public class AplicacaoRelatorioItemRepository : IAplicacaoRelatorioItemRepository
{
    private readonly ContextBase _contextBase;
    private readonly IDbConnection _dbConnection;

    public AplicacaoRelatorioItemRepository(ContextBase contextBase, IDbConnection dbConnection)
    {
        _contextBase = contextBase;
        _dbConnection = dbConnection;   
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj)
    {
        await _contextBase.AddAsync(obj);
        await _contextBase.SaveChangesAsync();
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>> GetAllAsync(int idAplicacaoRelatorio)
    {
        var entities = await _contextBase.AplicacaoRelatorioItem.ToListAsync();
        return entities;
    }


     public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>> GetAllByAplicacaoRelatorioIdAsync(int idAplicacaoRelatorio)
    {
        //var entities = await _contextBase.AplicacaoRelatorioItem.Where(aplicacaoRelatorioItem=> aplicacaoRelatorioItem.IdAplicacaoRelatorio == aplicacaoRelatorioId).ToListAsync();
        //return entities;

        string query = "SELECT * FROM AplicacaoRelatorioItem WHERE IdAplicacaoRelatorio = @IdAplicacaoRelatorio";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(query, new { IdAplicacaoRelatorio = idAplicacaoRelatorio });
    }


    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRelatorioItem.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> obj)
    {
        foreach (var item in obj)
        {

            var objeto = await _contextBase.AplicacaoRelatorioItem.FindAsync(item.Id);
            if(objeto != null)
            {
                objeto.IdAplicacaoRelatorio = item.IdAplicacaoRelatorio;
                objeto.HoraInicio = item.HoraInicio;
                objeto.HorimetroInicial = item.HorimetroInicial;
                objeto.HoraTermino = item.HoraTermino;
                objeto.HorimetroTermino = item.HorimetroTermino;
                objeto.TemperaturaInicial = item.TemperaturaInicial;
                objeto.TemperaturaFinal = item.TemperaturaFinal;
                objeto.UrInicial = item.UrInicial;
                objeto.UrFinal = item.UrFinal;
                objeto.VentoInicial = item.VentoInicial;
                objeto.VentoFinal = item.VentoFinal;
                objeto.ImagemDadosClimaticos = item.ImagemDadosClimaticos;

                _contextBase.AplicacaoRelatorioItem.Update(objeto);
            } 
        }
        
        await _contextBase.SaveChangesAsync();
    }
}
