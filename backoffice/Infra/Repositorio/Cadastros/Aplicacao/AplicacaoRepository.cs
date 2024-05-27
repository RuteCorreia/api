using Domain.Interfaces.Cadastros.Aplicacao;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.Aplicacao;

public class AplicacaoRepository : IAplicacaoRepository
{
    private readonly ContextBase _contextBase;

    public AplicacaoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(Domain.Entidades.Cadastros.Aplicacao.Aplicacao obj)
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

    public async Task<IEnumerable<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>> GetAllAsync()
    {
        //var entities = await _contextBase.Aplicacao.ToListAsync();
        return null;
    }

    public async Task<Domain.Entidades.Cadastros.Aplicacao.Aplicacao> GetByIdAsync(int id)
    {
        //var obj = await _contextBase.Aplicacao.FindAsync(id);
        return null;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.Aplicacao.Aplicacao obj)
    {
        //var objeto = await _contextBase.Aplicacao.FindAsync(obj.Id);
        //objeto.IdEmpresa = obj.IdEmpresa;
        //objeto.StatusEnvio = obj.StatusEnvio;
        //objeto.IdPiloto = obj.IdPiloto;
        //objeto.IdExecutor = obj.IdExecutor;
        //objeto.IdCliente = obj.IdCliente;
        //objeto.IdCultura = obj.IdCultura;

        //_contextBase.Aplicacao.Update(objeto);
        //await _contextBase.SaveChangesAsync();
    }
}
