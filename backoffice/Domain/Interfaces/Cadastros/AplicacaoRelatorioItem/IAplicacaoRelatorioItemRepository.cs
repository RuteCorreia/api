using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;

public interface IAplicacaoRelatorioItemRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem> GetByIdAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoRelatorioItem>> GetAllByAplicacaoRelatorioIdAsync(int aplicacaoRelatorioId);
}
