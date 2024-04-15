using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Infra.Configuracao;

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

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj)
    {
        throw new NotImplementedException();
    }
}
