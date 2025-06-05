using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;

namespace Application.DTOs.Cadastros.ReceituarioAgronomico.Interface;

public interface IReceituarioAgronomicoService
{
    Task<int> AddAsync(ReceituarioAgronomicoViewModel obj);
    Task UpdateAsync(ReceituarioAgronomicoViewModel obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<ReceituarioAgronomicoViewModel>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId);
    Task<ReceituarioAgronomicoViewModel> GetByIdAsync(int id);
}
