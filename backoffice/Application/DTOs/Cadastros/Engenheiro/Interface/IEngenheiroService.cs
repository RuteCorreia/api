using Application.DTOs.Cadastros.Engenheiro.ViewModel;

namespace Application.DTOs.Cadastros.Engenheiro.Interface;

public interface IEngenheiroService 
{
    Task<IEnumerable<EngenheiroViewModel>> GetAllAsync(string? idEmpresa);

    Task<EngenheiroViewModel?> GetByIdAsync(string id, string? idEmpresa);
}
