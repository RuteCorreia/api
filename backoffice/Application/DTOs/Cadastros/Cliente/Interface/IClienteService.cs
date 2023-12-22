using Application.DTOs.Cadastros.Cliente.ViewModel;

namespace Application.DTOs.Cadastros.Cliente.Interface;

public interface IClienteService 
{
    Task<IEnumerable<ClienteViewModel>> GetAllAsync();

    Task<ClienteViewModel> GetByIdAsync(int id);

    Task<ClienteViewModel> GetByLoginAsync(string email, string password);

    Task AddAsync(ClienteViewModel obj);

    Task UpdateAsync(ClienteViewModel obj);

    Task DeleteAsync(int id);
}
