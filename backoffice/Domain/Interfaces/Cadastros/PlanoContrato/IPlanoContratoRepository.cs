using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.PlanoDeContrato;

public interface IPlanoDeContratoRepository
{
    Task AddAsync(Entidades.Cadastros.Empresa.PlanoDeContrato obj);
    Task UpdateAsync(Entidades.Cadastros.Empresa.PlanoDeContrato obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Empresa.PlanoDeContrato>> GetAllAsync();
    Task<Entidades.Cadastros.Empresa.PlanoDeContrato> GetByIdAsync(int id);
}
