using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;

namespace Application.DTOs.Cadastros.AlvoBiologico.Interface;

public interface IAlvoBiologicoService 
{
    Task<IEnumerable<AlvoBiologicoViewModel>> GetAllAsync();

    Task<AlvoBiologicoViewModel> GetByIdAsync(int id);

    Task<IEnumerable<AlvoBiologicoViewModel>> GetByIdCulturaAsync(int id);

    Task<AlvoBiologicoViewModel> GetByName(string name);
    Task<List<string>> GetAlvosBiologicosAsync(string nomeCultura, string nomeProduto);

    Task AddAsync(AlvoBiologicoViewModel obj);

    Task UpdateAsync(AlvoBiologicoViewModel obj);

    Task DeleteAsync(int id);
}
