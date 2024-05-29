using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCroqui;

public interface IAplicacaoCroquiRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoCroqui obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoCroqui obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoCroqui>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoCroqui> GetByIdAsync(int id);
}
