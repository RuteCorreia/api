using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aplicacao;

public interface IAplicacaoRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.Aplicacao obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.Aplicacao obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.Aplicacao>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.Aplicacao> GetByIdAsync(int id);
}
