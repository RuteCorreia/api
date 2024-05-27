using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoLog;

public interface IAplicacaoLogRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoLog obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoLog obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoLog>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoLog> GetByIdAsync(int id);
}
