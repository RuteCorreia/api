using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;

public interface IAplicacaoRecomendacoesTecnicasService 
{
    Task<IEnumerable<AplicacaoRecomendacoesTecnicasViewModel>> GetAllAsync();

    Task<AplicacaoRecomendacoesTecnicasViewModel> GetByIdAsync(int id);
    Task<AplicacaoRecomendacoesTecnicasViewModel> GetForExportExcelAsync(int id);

    Task<int> AddAsync(AplicacaoRecomendacoesTecnicasViewModel obj, string? idEmpresa);

    Task UpdateAsync(AplicacaoRecomendacoesTecnicasViewModel obj);

    Task DeleteAsync(int id);
}
