using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.DataRelatorio.ViewModel;

namespace Application.DTOs.Cadastros.DataRelatorio.Interface
{
    public interface IDataRelatorioService
    {
        Task<IEnumerable<DataRelatorioViewModel>> GetAllAsync(string? idEmpresa);

        Task<DataRelatorioViewModel?> GetByIdAsync(int? id, string? idEmpresa);

        Task<int> AddAsync(DataRelatorioViewModel obj, string? idEmpresa);
        Task<int> UpdateAsync(DataRelatorioViewModel obj);

        Task DeleteAsync(int id, string? idEmpresa);
    }
}
