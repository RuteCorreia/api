using Application.DTOs.Cadastros.Alvo_Biologico.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;

namespace Application.DTOs.Cadastros.AlvoBiologico.Interface;

public interface IAlvoBiologicoService 
{
    Task<IEnumerable<AlvoBiologicoViewModel>> GetAllAsync();

    Task<AlvoBiologicoViewModel> GetByIdAsync(int id);

    Task<IEnumerable<AlvoBiologicoViewModel>> GetByIdCulturaAsync(int id);

    Task<AlvoBiologicoViewModel> GetByName(string name);
    Task<IEnumerable<AlvoBiologicoViewModel>> GetAlvosBiologicosAsync(string nomeCultura, string nomeProduto);
    Task<IEnumerable<FormulacaoViewModel>> GetFormulacaoAsync(int idBula);
    Task UpdateFormulacaoAsync(FormulacaoViewModel idBula);
    Task AddAsync(AlvoBiologicoViewModel obj);

    Task UpdateAsync(AlvoBiologicoViewModel obj);

    Task DeleteAsync(int id);
}
