using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AlvoBiologico;

public interface IAlvoBiologicoRepository
{
    Task AddAsync(Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj);
    Task UpdateAsync(Entidades.Cadastros.Alvo_Biologico.AlvoBiologico obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByIdAsync(int? id);
    Task<IEnumerable<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetByIdCulturaAsync(int id);
    Task<Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> GetByNameAsync(string name);
    Task<IEnumerable<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>> GetAlvosBiologicosAsync(int idProduto, int idCultura);
}
