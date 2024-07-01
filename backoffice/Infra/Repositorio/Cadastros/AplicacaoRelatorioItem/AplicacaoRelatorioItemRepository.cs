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

        string query = "SELECT * FROM RelatorioAplicacao WHERE IdAplicacaoRelatorio = @IdAplicacaoRelatorio";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>(query, new { DataCriacao = idAplicacaoRelatorio });
    }


    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRelatorioItem.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj)
    {
        var objeto = await _contextBase.AplicacaoRelatorioItem.FindAsync(obj.Id);
        objeto.IdAplicacaoRelatorio = obj.IdAplicacaoRelatorio;
        objeto.HoraInicio = obj.HoraInicio;
        objeto.HorimetroInicial = obj.HorimetroInicial;
        objeto.HoraTermino = obj.HoraTermino;
        objeto.HorimetroTermino = obj.HorimetroTermino;
        objeto.TemperaturaInicial = obj.TemperaturaInicial;
        objeto.TemperaturaFinal = obj.TemperaturaFinal;
        objeto.UrInicial = obj.UrInicial;
        objeto.UrFinal = obj.UrFinal;
        objeto.VentoInicial = obj.VentoInicial;
        objeto.VentoFinal = obj.VentoFinal;
        objeto.ImagemDadosClimaticos = obj.ImagemDadosClimaticos;

        _contextBase.AplicacaoRelatorioItem.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
