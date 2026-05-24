using Application.DTOs.Cadastros.Alvo_Biologico.ViewModel;
using Application.DTOs.Cadastros.AlvoBiologico.ViewModel;

namespace Application.DTOs.Cadastros.AlvoBiologico.Interface;

public interface IAlvoBiologicoService 
{
    Task<IEnumerable<AlvoBiologicoViewModel>> GetAllAsync(string? idEmpresa);

    Task<AlvoBiologicoViewModel> GetByIdAsync(int id);

    Task<IEnumerable<AlvoBiologicoViewModel>> GetByIdCulturaAsync(int id);

    Task<AlvoBiologicoViewModel> GetByName(string name, string? idEmpresa);
    Task<IEnumerable<AlvoBiologicoViewModel>> GetAlvosBiologicosAsync(string nomeCultura, string nomeProduto, string? idEmpresa);
    Task<IEnumerable<FormulacaoViewModel>> GetFormulacaoAsync(int idBula);
    Task UpdateFormulacaoAsync(FormulacaoViewModel idBula);
    Task AddAsync(AlvoBiologicoViewModel obj, string? idEmpresa);

    Task UpdateAsync(AlvoBiologicoViewModel obj, string? idEmpresa);

    Task DeleteAsync(int id, string? idEmpresa);
}
