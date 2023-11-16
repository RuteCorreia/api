using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoContrato;

public interface IAplicacaoContratoRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoContrato obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoContrato obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoContrato>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoContrato> GetByIdAsync(int id);
}
