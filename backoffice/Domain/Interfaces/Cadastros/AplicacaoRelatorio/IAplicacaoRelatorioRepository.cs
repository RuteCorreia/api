using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoRelatorio;

public interface IAplicacaoRelatorioRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoRelatorio obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoRelatorio obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoRelatorio>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoRelatorio> GetByIdAsync(int id);
}
