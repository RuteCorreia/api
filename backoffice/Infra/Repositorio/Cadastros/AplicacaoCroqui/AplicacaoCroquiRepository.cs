using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoCroqui;

public class AplicacaoCroquiRepository : IAplicacaoCroquiRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoCroquiRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoCroqui.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoCroqui.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui obj)
    {
        var objeto = await _contextBase.AplicacaoCroqui.FindAsync(obj.Id);
        objeto.IdAplicacao = obj.IdAplicacao;
        objeto.Desenho = obj.Desenho;
        objeto.IdMapa = obj.IdMapa;
        objeto.Latitude = obj.Latitude;
        objeto.Longitude = obj.Longitude;

        _contextBase.AplicacaoCroqui.Update(objeto);
        await _contextBase.SaveChangesAsync();
    }
}
