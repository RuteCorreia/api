using Application.DTOs.Cadastros.Aplicacao.ViewModel;

namespace Application.DTOs.Cadastros.Aplicacao.Interface;

public interface IAplicacaoService 
{
    Task<IEnumerable<AplicacaoViewModel>> GetAllAsync();

    Task<AplicacaoViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoViewModel obj);

    Task UpdateAsync(AplicacaoViewModel obj);

    Task DeleteAsync(int id);
}
