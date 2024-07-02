using Application.DTOs.Cadastros.CombateIncendio.ViewModel;
using Application.DTOs.Cadastros.CombateIncendioPista.ViewModel;
using Domain.Entidades.Cadastros.CombateIncendio;

namespace Application.DTOs.Cadastros.CombateIncendioPista.Interface
{
    public interface ICombateIncendioPistaService
    {
        Task<IEnumerable<CombateIncendioPistaViewModel>> GetAllAsync(string? idEmpresa);

        Task<CombateIncendioPistaViewModel> GetByIdAsync(int id);

        Task<int> AddAsync(CombateIncendioPistaViewModel obj, string? idEmpresa);

        Task<int> UpdateAsync(CombateIncendioPistaViewModel obj, string? idEmpresa);
        Task<IEnumerable<CombateIncendioPistaViewModel>> GetByCombateIncendioIdAsync(int combateIncendioId);

        Task DeleteAsync(int id);
    }
}
