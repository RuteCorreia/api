using Domain.Interfaces.Cadastros.CombateIncendio;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.CombateIncendio;

public class CombateIncendioRepository : ICombateIncendioRepository
{
    private readonly ContextBase _contextBase;

    public CombateIncendioRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync()
    {
        var entities = await _contextBase.CombateIncendio.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id)
    {
        var obj = await _contextBase.CombateIncendio.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio obj)
    {
        var objeto = await _contextBase.CombateIncendio.FindAsync(obj.Id);
        objeto.IdEmpresa = obj.IdEmpresa;
        objeto.IdExecutor = obj.IdExecutor;
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

        _contextBase.CombateIncendio.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
