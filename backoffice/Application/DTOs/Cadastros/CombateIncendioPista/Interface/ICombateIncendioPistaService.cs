using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;

namespace Application.DTOs.Cadastros.CombateIncendioPista.Interface
{
    public interface ICombateIncendioPistaService
    {
        Task<IEnumerable<CombateIncendioPistaViewModel>> GetAllAsync(string? idEmpresa);

        Task<CombateIncendioPistaViewModel> GetByIdAsync(int id);

        Task<int> AddAsync(CombateIncendioPistaViewModel obj, string? idEmpresa);

        Task<int> UpdateAsync(CombateIncendioPistaViewModel obj, string? idEmpresa);

        Task DeleteAsync(int id);
    }
}
