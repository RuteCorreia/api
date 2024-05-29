using Application.DTOs.Cadastros.Cliente.ViewModel;

namespace Application.DTOs.Cadastros.Cliente.Interface;

public interface IClienteService 
{
    Task<IEnumerable<ClienteViewModel>> GetAllAsync(string? idEmpresa);

    Task<ClienteViewModel> GetByIdAsync(int id, string? idEmpresa);

    Task<ClienteViewModel> GetByLoginAsync(string email, string password);

    Task AddAsync(ClienteViewModel obj, string? idEmpresa);

    Task UpdateAsync(ClienteViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);
}
