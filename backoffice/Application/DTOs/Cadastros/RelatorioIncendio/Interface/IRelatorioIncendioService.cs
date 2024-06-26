using Application.DTOs.Cadastros.RelatorioIncendio.ViewModel;

namespace Application.DTOs.Cadastros.RelatorioIncendio.Interface
{
    public interface IRelatorioIncendioService
    {
        Task<IEnumerable<RelatorioIncendioViewModel>> GetAllAsync();

        Task<RelatorioIncendioViewModel> GetByIdAsync(int id);

        Task AddAsync(RelatorioIncendioViewModel obj);

        Task UpdateAsync(RelatorioIncendioViewModel obj);

        Task DeleteAsync(int id);
    }
}
