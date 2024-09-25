using Application.DTOs.Cadastros.FrotaBateria.ViewModel;
using Application.DTOs.Cadastros.FrotaGerador.ViewModel;

namespace Application.DTOs.Cadastros.FrotaGerador.Interface
{
    public interface IFrotaGeradorService
    {
        Task<int> AddAsync(FrotaGeradorViewModel obj, string? idEmpresa);
        Task<int> UpdateAsync(FrotaGeradorViewModel obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<FrotaGeradorViewModel>> GetAllAsync(string? idEmpresa);
        Task<IEnumerable<FrotaGeradorViewModel>> GetByIdAsync(int? id);
    }
}
