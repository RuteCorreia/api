using Dapper;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Infra.Repositorio.Cadastros.CombateIncendio;

public class CombateIncendioRepository : ICombateIncendioRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ContextBase _contextBase;

    public CombateIncendioRepository(IDbConnection dbConnection, ContextBase contextBase)
    {
        _dbConnection = dbConnection;
        _contextBase = contextBase;
    }

    public async Task<int> AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync(DateTime? offsetDate, int idEmpresa)
    {
        string query = "SELECT * FROM CombateIncendio" +
                           " WHERE " +
                           (offsetDate != null ? " ( CONVERT(VARCHAR, DataAlteracao, 120) > CONVERT(VARCHAR, @offsetDate, 120) OR CONVERT(VARCHAR, DataCriacao, 120) > CONVERT(VARCHAR, @offsetDate, 120)) AND " : " ") +
                           " IdEmpresa = @IdEmpresa";
        Console.WriteLine(query);
        if (offsetDate != null)
        {
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(query, new { offsetDate = offsetDate, IdEmpresa = idEmpresa });
        }
        else
        {
            return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(query, new { IdEmpresa = idEmpresa });
        }
    }

    public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id)
    {
        var obj = await _contextBase.CombateIncendio.FindAsync(id);
        return obj;
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetListByStatusAsync(int idEmpresa, int statusEnvio)
    {
        string query = "SELECT * FROM CombateIncendio WHERE IdEmpresa = @IdEmpresa AND StatusEnvio = @statusEnvio AND IsMapa = 0";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio });
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetListByStatusMapaAsync(int idEmpresa, int statusEnvio)
    {
        string query = "SELECT * FROM CombateIncendio WHERE IdEmpresa = @IdEmpresa AND StatusEnvio = @statusEnvio AND IsMapa = 1";
        return await _dbConnection.QueryAsync<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>(query, new { IdEmpresa = idEmpresa, StatusEnvio = statusEnvio });
    }

    public async Task UpdateIsMapaAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
    {
        var objeto = await _contextBase.CombateIncendio.FindAsync(obj.Id);
        objeto.IsMapa = obj.IsMapa;
        _contextBase.CombateIncendio.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
    {
        var objeto = await _contextBase.CombateIncendio.FindAsync(obj.Id);
        objeto.IdEmpresa = obj.IdEmpresa;
        if (obj.IdExecutor != Guid.Empty)
        {
            objeto.IdExecutor = obj.IdExecutor;
        }
        objeto.OrgaoPublico_Privado = obj.OrgaoPublico_Privado;
        objeto.Aviso = obj.Aviso;
        objeto.IdAeronave = obj.IdAeronave;
        objeto.IdPista = obj.IdPista;
        objeto.Data = obj.Data;
        objeto.HoraInicial = obj.HoraInicial;
        objeto.HorimetroAviao = obj.HorimetroAviao;
        objeto.LocalIncendioLat = obj.LocalIncendioLat;
        objeto.LocalIncendioLon = obj.LocalIncendioLon;
        objeto.Referencia = obj.Referencia;
        objeto.HorarioFinalOperacao = obj.HorarioFinalOperacao;
        objeto.HorimetroFinalOperacao = obj.HorimetroFinalOperacao;
        objeto.TotalAguaUtilizadaOperacao = obj.TotalAguaUtilizadaOperacao;
        objeto.CoordenadorBaseOperacionalNome = obj.CoordenadorBaseOperacionalNome;
        objeto.CoordenadorBaseOperacionalPosto = obj.CoordenadorBaseOperacionalPosto;
        objeto.CoordenadorBaseOperacionalRE = obj.CoordenadorBaseOperacionalRE;
        objeto.CoordenadorBaseOperacionalAssinatura = obj.CoordenadorBaseOperacionalAssinatura;
        objeto.ComandanteOcorrenciaNome = obj.ComandanteOcorrenciaNome;
        objeto.ComandanteOcorrenciaPosto = obj.ComandanteOcorrenciaPosto;
        objeto.ComandanteOcorrenciaRE = obj.ComandanteOcorrenciaRE;
        objeto.ComandanteOcorrenciaAssinatura = obj.ComandanteOcorrenciaAssinatura;
        objeto.ResponsavelOcorrenciaNome = obj.ResponsavelOcorrenciaNome;
        objeto.ResponsavelOcorrenciaPosto = obj.ResponsavelOcorrenciaPosto;
        objeto.ResponsavelOcorrenciaRE = obj.ResponsavelOcorrenciaRE;
        objeto.ResponsavelOcorrenciaAssinatura = obj.ResponsavelOcorrenciaAssinatura;
        objeto.Cidade = obj.Cidade;
        objeto.Cliente = obj.Cliente;
        objeto.Uf = obj.Uf;
        objeto.Observacao = obj.Observacao;
        objeto.CapacidadeCargaAeronave = obj.CapacidadeCargaAeronave;
        objeto.Piloto = obj.Piloto;
        if (obj.ContratoPrestacaoServicoId != null)
        {
            objeto.ContratoPrestacaoServicoId = obj.ContratoPrestacaoServicoId;
        }
        objeto.DataCriacao = obj.DataCriacao;
        objeto.DataAlteracao = obj.DataAlteracao;
        objeto.StatusEnvio = obj.StatusEnvio;

        _contextBase.CombateIncendio.Update(objeto);
        try
        {
            await _contextBase.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        
        return objeto.Id;
    }
}
