using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Precificacao;

public interface IPrecificacaoRepository
{
    Task AddAsync(Entidades.Cadastros.Precificacao.Precificacao obj);
    Task UpdateAsync(Entidades.Cadastros.Precificacao.Precificacao obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Precificacao.Precificacao>> GetAllAsync();
    Task<Entidades.Cadastros.Precificacao.Precificacao> GetByIdAsync(int id);
}
