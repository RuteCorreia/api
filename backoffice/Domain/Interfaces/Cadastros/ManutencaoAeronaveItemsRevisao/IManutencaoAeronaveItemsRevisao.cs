namespace Domain.Interfaces.Cadastros.ManutencaoAeronaveItemsRevisao;

public interface IManutencaoAeronaveItemsRevisao
{
    Task AddAsync(Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj);
    Task UpdateAsync(Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao>> GetAllAsync();
    Task<Entidades.Cadastros.ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao> GetByIdAsync(int id);
}
