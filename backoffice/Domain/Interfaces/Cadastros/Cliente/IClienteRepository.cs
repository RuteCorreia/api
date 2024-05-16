namespace Domain.Interfaces.Cadastros.Cliente;

public interface IClienteRepository
{
    Task AddAsync(Entidades.Cadastros.Cliente.Cliente obj);
    Task UpdateAsync(Entidades.Cadastros.Cliente.Cliente obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Cliente.Cliente>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.Cliente.Cliente> GetByIdAsync(int id, int idEmpresa);
    Task<Entidades.Cadastros.Cliente.Cliente> GetByLoginAsync(string email, string password);
}
