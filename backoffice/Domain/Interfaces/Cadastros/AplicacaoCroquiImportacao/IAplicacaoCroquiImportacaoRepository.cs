using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;

public interface IAplicacaoCroquiImportacaoRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> GetByIdAsync(int id);
}
