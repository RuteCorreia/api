using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;

public interface IAplicacaoCaracteristicasRepository
{
    Task AddAsync(Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj);
    Task UpdateAsync(Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>> GetAllAsync();
    Task<Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> GetByIdAsync(int id);
}
