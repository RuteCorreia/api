using Application.DTOs.Cadastros.Bateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;

namespace Application.DTOs.Cadastros.Gerador.Interface
{
    public interface IGeradorService
    {
        Task<int> AddAsync(GeradorViewModel obj, string? idEmpresa);
        Task UpdateAsync(GeradorViewModel obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<GeradorViewModel>> GetAllAsync(string? idEmpresa);
        Task<GeradorViewModel> GetByIdAsync(int? id);
    }
}
