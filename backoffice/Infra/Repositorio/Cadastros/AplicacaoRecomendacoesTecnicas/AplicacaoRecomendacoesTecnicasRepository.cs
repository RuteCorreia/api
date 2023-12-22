using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoRecomendacoesTecnicas;

public class AplicacaoRecomendacoesTecnicasRepository : IAplicacaoRecomendacoesTecnicasRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoRecomendacoesTecnicasRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoRecomendacoesTecnicas.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoRecomendacoesTecnicas.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj)
    {
        var objeto = await _contextBase.AplicacaoRecomendacoesTecnicas.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.IdVeiculante = obj.IdVeiculante;
        objeto.QtdeVeiculante = obj.QtdeVeiculante;
        objeto.LarguraFaixa = obj.LarguraFaixa;
        objeto.VolumeAplicacao = obj.VolumeAplicacao;
        objeto.IdAeronave = obj.IdAeronave;
        objeto.IdAlturaVoo = obj.IdAlturaVoo;
        objeto.AlturaVooCustom = obj.AlturaVooCustom;
        objeto.Temperatura = obj.Temperatura;
        objeto.UrDoAR = obj.UrDoAR;
        objeto.VelocidadeVento = obj.VelocidadeVento;
        objeto.IdTipoDeProduto = obj.IdTipoDeProduto;
        objeto.IdEquipamento = obj.IdEquipamento;
        objeto.Angulo = obj.Angulo;

        _contextBase.AplicacaoRecomendacoesTecnicas.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
