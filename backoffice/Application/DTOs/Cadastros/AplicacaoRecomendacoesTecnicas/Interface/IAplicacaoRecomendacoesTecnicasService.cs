using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;

public interface IAplicacaoRecomendacoesTecnicasService 
{
    Task<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>> GetAllAsync();

    Task<AplicacaoRecomendacoesTecnicasViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoRecomendacoesTecnicasViewModel obj);

    Task UpdateAsync(AplicacaoRecomendacoesTecnicasViewModel obj);

    Task DeleteAsync(int id);
}
