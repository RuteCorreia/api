using Dapper;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Helpers;
using Infra.Configuracao;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoRelatorio;

public class AplicacaoRelatorioRepository : IAplicacaoRelatorioRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoRelatorioRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoRelatorio.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRelatorio.FindAsync(id);
        return obj;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> GetForExportExcelAsync(int? id)
    {
        var query = @"
            SELECT Id, Dosagem, TotalAreaAplicada, VolumeAplicacao
            FROM AplicacaoRelatorio WHERE Id = @Id";

        using (var connection = new SqlConnection(_contextBase.ObterStringConexao()))
        {
            var parameters = new { Id = id };
            var result = await connection.QueryFirstOrDefaultAsync<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>(query, parameters);
            return result;
        }
    }
    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio obj)
    {
        var objeto = await _contextBase.AplicacaoRelatorio.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.IdPista = obj.IdPista;
        objeto.Dosagem = obj.Dosagem;
        objeto.KG_LT = obj.KG_LT;
        objeto.VolumeAplicacao = obj.VolumeAplicacao;
        objeto.TotalAreaAplicada = obj.TotalAreaAplicada;
        objeto.Alteracoes_Observacoes = obj.Alteracoes_Observacoes;
        objeto.Cultura = obj.Cultura;
        objeto.Densidade = obj.Densidade;
        objeto.Latitude = obj.Latitude;
        objeto.Longitude = obj.Longitude;
        objeto.LocalizacaoPistaCodigoICAO = obj.LocalizacaoPistaCodigoICAO;
        objeto.ProdutoAplicado = obj.ProdutoAplicado;
        objeto.RelatorioDGPS = obj.RelatorioDGPS;
        objeto.UnidadeVolumeAplicacao = obj.UnidadeVolumeAplicacao;
        objeto.MapaAplicacao = obj.MapaAplicacao;

        _contextBase.AplicacaoRelatorio.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
