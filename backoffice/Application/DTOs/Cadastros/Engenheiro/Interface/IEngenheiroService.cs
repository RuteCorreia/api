using Application.DTOs.Cadastros.Cliente.ViewModel;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;

namespace Application.DTOs.Cadastros.Engenheiro.Interface;

public interface IEngenheiroService 
{
    Task<IEnumerable<EngenheiroViewModel>> GetAllAsync();

    Task<EngenheiroViewModel> GetByIdAsync(string id);
}
