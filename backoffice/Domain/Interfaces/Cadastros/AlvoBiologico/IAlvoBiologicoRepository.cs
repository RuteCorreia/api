using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AlvoBiologico;

public interface IAlvoBiologicoRepository
{
    Task AddAsync(Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj);
    Task UpdateAsync(Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync();
    Task<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int id);
    Task<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByNameAsync(string name);
}
