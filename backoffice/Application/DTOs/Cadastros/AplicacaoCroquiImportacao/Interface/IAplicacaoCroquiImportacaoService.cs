using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Interface;

public interface IAplicacaoCroquiImportacaoService 
{
    Task<IEnumerable<AplicacaoCroquiImportacaoViewModel>> GetAllAsync();

    Task<AplicacaoCroquiImportacaoViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoCroquiImportacaoViewModel obj);

    Task UpdateAsync(AplicacaoCroquiImportacaoViewModel obj);

    Task DeleteAsync(int id);
}
