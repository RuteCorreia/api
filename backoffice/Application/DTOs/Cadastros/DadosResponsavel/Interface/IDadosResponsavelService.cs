using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;

namespace Application.DTOs.Cadastros.DadosResponsavel.Interface;

public interface IDadosResponsavelService
{
    Task<IEnumerable<DadosResponsavelViewModel>> GetAllAsync(string? idEmpresa);

    Task<DadosResponsavelViewModel?> GetByIdAsync(int id, string? idEmpresa);

    Task<int> AddAsync(DadosResponsavelViewModel obj, string? idEmpresa);

    Task UpdateAsync(DadosResponsavelViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);
}
