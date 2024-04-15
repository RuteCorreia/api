namespace Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;

public interface IManutencaoAeronaveItemsRevisaoRepository
{
    Task AddAsync(IEnumerable<Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao> obj);
    Task UpdateAsync(Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj);
    Task DeleteByIdManutencaoAeronaveAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllAsync();
    Task<IEnumerable<Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllByManutencaoAeronaveIdAsync(int id);
}
