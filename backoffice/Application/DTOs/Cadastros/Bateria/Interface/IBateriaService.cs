using Application.DTOs.Cadastros.Bateria.ViewModel;
using Domain.Entidades.Cadastros.Empresa;

namespace Application.DTOs.Cadastros.Bateria.Interface
{
    public interface IBateriaService
    {
        Task<int> AddAsync(BateriaViewModel obj, string? idEmpresa);
        Task UpdateAsync(BateriaViewModel obj);
        Task DeleteAsync(int id, string? idEmpresa);
        Task<IEnumerable<BateriaViewModel>> GetAllAsync(string? idEmpresa);
        Task<BateriaViewModel> GetByIdAsync(int? id, string? idEmpresa);
        Task<IEnumerable<BateriaViewModel>> GetByNameAsync(string name, string? idEmpresa);
    }
}
