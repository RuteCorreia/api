using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.Gerador.ViewModel;

namespace Application.DTOs.Cadastros.FrotaBateria.Interface
{
    public interface IFrotaBateriaService
    {
        Task<int> AddAsync(FrotaBateriaViewModel obj, string? idEmpresa);
        Task UpdateAsync(FrotaBateriaViewModel obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<FrotaBateriaViewModel>> GetAllAsync(string? idEmpresa);
        Task<FrotaBateriaViewModel> GetByIdAsync(int? id);
    }
}
