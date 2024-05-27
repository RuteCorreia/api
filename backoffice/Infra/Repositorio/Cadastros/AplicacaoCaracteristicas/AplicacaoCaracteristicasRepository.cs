using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoCaracteristicas;

public class AplicacaoCaracteristicasRepository : IAplicacaoCaracteristicasRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoCaracteristicasRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoCaracteristicas.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoCaracteristicas.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj)
    {
        var objeto = await _contextBase.AplicacaoCaracteristicas.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.IdProduto = obj.IdProduto;
        objeto.IdAdjuvante = obj.IdAdjuvante;
        objeto.TipoDeServico = obj.TipoDeServico;

        _contextBase.AplicacaoCaracteristicas.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
