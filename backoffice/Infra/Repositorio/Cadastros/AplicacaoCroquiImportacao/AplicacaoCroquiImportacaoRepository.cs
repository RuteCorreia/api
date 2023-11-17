using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.AplicacaoCroquiImportacao;

public class AplicacaoCroquiImportacaoRepository : IAplicacaoCroquiImportacaoRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoCroquiImportacaoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>> GetAllAsync()
    {
        var entities = await _contextBase.AplicacaoCroquiImportacao.ToListAsync();
        return entities;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> GetByIdAsync(int id)
    {
        var obj = await _contextBase.AplicacaoCroquiImportacao.FindAsync(id);
        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao obj)
    {
        _contextBase.AplicacaoCroquiImportacao.Update(obj);
        await _contextBase.SaveChangesAsync();
    }
}
