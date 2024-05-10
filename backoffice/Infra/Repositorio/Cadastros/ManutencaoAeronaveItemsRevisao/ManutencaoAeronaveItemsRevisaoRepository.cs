using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.Cadastros.ManutencaoAeronaveItemsRevisao;

public class ManutencaoAeronaveItemsRevisaoRepository : IManutencaoAeronaveItemsRevisaoRepository
{
    private readonly ContextBase _contextBase;

    public ManutencaoAeronaveItemsRevisaoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public async Task AddAsync(IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao> obj)
    {
        await _contextBase.ManutencaoAeronaveItemsRevisao.AddRangeAsync(obj);
        await _contextBase.SaveChangesAsync();
    }

    public async Task DeleteByIdManutencaoAeronaveAsync(int id)
    {
        var itens = _contextBase.ManutencaoAeronaveItemsRevisao.Where(x => x.IdManutencaoAeronave == id);
        if(itens.Any())
        {
            _contextBase.ManutencaoAeronaveItemsRevisao.RemoveRange(itens);
            await _contextBase.SaveChangesAsync();
        }
    }

    public Task<IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllByManutencaoAeronaveIdAsync(
        int id
    )
    {
        var obj = await _contextBase.ManutencaoAeronaveItemsRevisao
            .AsNoTracking()
            .Where(x => x.IdManutencaoAeronave == id)
            .ToListAsync();

        return obj;
    }

    public async Task UpdateAsync(Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj)
    {
        throw new NotImplementedException();
    }
}
