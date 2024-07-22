using Domain.Enums;

namespace Domain.Interfaces.Cadastros.Empresa;

public interface IEmpresaRepository
{
    Task AddAsync(Entidades.Cadastros.Empresa.Empresa obj);
    Task UpdateAsync(Entidades.Cadastros.Empresa.Empresa obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Empresa>> GetAllAsync();
    Task<Entidades.Cadastros.Empresa.Empresa> GetByIdAsync(int id);
    Task<Domain.Entidades.Cadastros.Empresa.Empresa> GetByEmailAsync(string email);
    Task<string> GetLogoByIdAsync(int id);
    Task ChangeStatusAsync(int id, EStatusEmpresa status);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Empresa>> GetByNameAsync(string name);
}
