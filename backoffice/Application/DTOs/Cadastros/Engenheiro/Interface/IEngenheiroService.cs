using Application.DTOs.Cadastros.Cliente.ViewModel;
using Application.DTOs.Cadastros.Engenheiro.ViewModel;

namespace Application.DTOs.Cadastros.Engenheiro.Interface;

public interface IEngenheiroService 
{
    Task<IEnumerable<EngenheiroViewModel>> GetAllAsync();

    Task<EngenheiroViewModel> GetByIdAsync(string id);

    Task<EngenheiroViewModel> GetByLoginAsync(string email, string password);
    Task<EngenheiroViewModel> GetByIdEmpresaAsync(int id, int idEmpresa);

    Task AddAsync(EngenheiroViewModel obj);

    Task UpdateAsync(EngenheiroViewModel obj);

    Task DeleteAsync(int id);
}
