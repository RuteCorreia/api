using Application.DTOs.Cadastros.FrotaGerador.ViewModel;
using Application.DTOs.Cadastros.FrotaMotobomba.ViewModel;

namespace Application.DTOs.Cadastros.FrotaMotobomba.Interface
{
    public interface IFrotaMotobombaService
    {
        Task<int> AddAsync(FrotaMotobombaViewModel obj, string? idEmpresa);
        Task UpdateAsync(FrotaMotobombaViewModel obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<FrotaMotobombaViewModel>> GetAllAsync(string? idEmpresa);
        Task<FrotaMotobombaViewModel> GetByIdAsync(int? id);
    }
}
