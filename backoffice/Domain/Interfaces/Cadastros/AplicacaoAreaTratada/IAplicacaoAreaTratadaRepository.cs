using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoAreaTratada;

public interface IAplicacaoAreaTratadaRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> GetByIdAsync(int id);
}
