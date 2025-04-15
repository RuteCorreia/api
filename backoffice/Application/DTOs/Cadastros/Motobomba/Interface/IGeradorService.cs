using Application.DTOs.Cadastros.Motobomba.ViewModel;

namespace Application.DTOs.Cadastros.Motobomba.Interface
{
    public interface IMotobombaService
    {
        Task<int> AddAsync(MotobombaViewModel obj, string? idEmpresa);
        Task UpdateAsync(MotobombaViewModel obj);
        Task DeleteAsync(int id, string? idEmpresa);
        Task<IEnumerable<MotobombaViewModel>> GetAllAsync(string? idEmpresa);
        Task<MotobombaViewModel> GetByIdAsync(int? id, string? idEmpresa);
        Task<IEnumerable<MotobombaViewModel>> GetByNameAsync(string name, string? idEmpresa);
    }
}
