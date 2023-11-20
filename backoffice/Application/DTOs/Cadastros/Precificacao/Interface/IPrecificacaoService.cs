using Application.DTOs.Cadastros.Precificacao.ViewModel;

namespace Application.DTOs.Cadastros.Precificacao.Interface;

public interface IPrecificacaoService 
{
    Task<IEnumerable<PrecificacaoViewModel>> GetAllAsync();

    Task<PrecificacaoViewModel> GetByIdAsync(int id);

    Task AddAsync(PrecificacaoViewModel obj);

    Task UpdateAsync(PrecificacaoViewModel obj);

    Task DeleteAsync(int id);
}
