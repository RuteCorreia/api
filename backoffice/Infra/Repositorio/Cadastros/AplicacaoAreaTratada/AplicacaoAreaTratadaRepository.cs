using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoAreaTratada;

public class AplicacaoAreaTratadaRepository : IAplicacaoAreaTratadaRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoAreaTratadaRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoAreaTratada.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoAreaTratada.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj)
    {
        var objeto = await _contextBase.AplicacaoAreaTratada.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.IdEstado = obj.IdEstado;
        objeto.IdCidade = obj.IdCidade;
        objeto.Localizacao = obj.Localizacao;
        objeto.Extensao = obj.Extensao;

        _contextBase.AplicacaoAreaTratada.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
