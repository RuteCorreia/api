using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Bula;

public interface IBulaRepository
{
    Task AddAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task UpdateAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task DeleteAsync(int id);
    Task RemoveRecomendacaoAsync(int idBula);
    Task RemoveBulaAsync(int idProduto, int idEmpresa);
    Task<IEnumerable<Domain.Entidades.Cadastros.Empresa.Bula>> GetByIdProdutoAsync(int idProduto, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Bula>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id);
    Task<Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name, int idEmpresa);
    Task<IEnumerable<int>> GetDistinctBulaAsync(int idEmpresa, string? nomeProduto);
}
