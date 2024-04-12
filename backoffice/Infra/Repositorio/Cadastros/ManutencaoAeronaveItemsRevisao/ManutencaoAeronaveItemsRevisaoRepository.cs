using Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.ManutencaoAeronaveItemsRevisao;

public class ManutencaoAeronaveItemsRevisaoRepository : IManutencaoAeronaveItemsRevisao
{
    private readonly ContextBase _contextBase;

    public ManutencaoAeronaveItemsRevisaoRepository(ContextBase contextBase)
    {
        _contextBase = contextBase;
    }

    public Task AddAsync(Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj)
    {
        throw new NotImplementedException();
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
