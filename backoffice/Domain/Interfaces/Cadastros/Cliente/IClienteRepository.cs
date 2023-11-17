using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Cliente;

public interface IClienteRepository
{
    Task AddAsync(Entidades.Cadastros.Cliente.Cliente obj);
    Task UpdateAsync(Entidades.Cadastros.Cliente.Cliente obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Cliente.Cliente>> GetAllAsync();
    Task<Entidades.Cadastros.Cliente.Cliente> GetByIdAsync(int id);
}
