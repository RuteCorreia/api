using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;

namespace Application.DTOs.Cadastros.TipoDeFormulacao.Interfaces
{
    public interface ITipoDeFormulacaoService
    {
        Task<IEnumerable<TipoDeFormulacaoViewModel>> GetAllAsync(string? idEmpresa);

        Task<TipoDeFormulacaoViewModel> GetByIdAsync(int id);
        Task<TipoDeFormulacaoViewModel> GetByNameAsync(string name, string? idEmpresa);

        Task AddAsync(TipoDeFormulacaoViewModel obj, string? idEmpresa);

        Task UpdateAsync(TipoDeFormulacaoViewModel obj, string? idEmpresa);

        Task DeleteAsync(int id, string? idEmpresa);
    }
}
