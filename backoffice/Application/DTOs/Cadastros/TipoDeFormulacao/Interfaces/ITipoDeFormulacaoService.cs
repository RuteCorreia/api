using Application.DTOs.Cadastros.TipoDeFormulacao.ViewModel;

namespace Application.DTOs.Cadastros.TipoDeFormulacao.Interfaces
{
    public interface ITipoDeFormulacaoService
    {
        Task<IEnumerable<TipoDeFormulacaoViewModel>> GetAllAsync();

        Task<TipoDeFormulacaoViewModel> GetByIdAsync(int id);

        Task AddAsync(TipoDeFormulacaoViewModel obj);

        Task UpdateAsync(TipoDeFormulacaoViewModel obj);

        Task DeleteAsync(int id);
    }
}
