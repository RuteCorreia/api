using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Helpers;
using Infra.Configuracao;
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
        var entityToRemove = GetByIdAsync(id);
        if(ObjectNullValidation.IsObjectNull(entityToRemove))
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

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoRelatorio obj)
    {
        _contextBase.AplicacaoRelatorio.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
