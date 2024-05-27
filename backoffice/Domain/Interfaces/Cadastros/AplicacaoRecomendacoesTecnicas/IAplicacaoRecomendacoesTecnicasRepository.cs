using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;

public interface IAplicacaoRecomendacoesTecnicasRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoRecomendacoesTecnicas> GetByIdAsync(int id);
}
