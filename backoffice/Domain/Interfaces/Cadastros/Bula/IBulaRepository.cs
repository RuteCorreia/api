using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Bula;

public interface IBulaRepository
{
    Task AddAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task UpdateAsync(Entidades.Cadastros.Empresa.Bula obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task RemoveRecomendacaoAsync(int idBula, int idEmpresa);
    Task RemoveBulaAsync(int idProduto, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Bula>> GetByIdProdutoAsync(int idProduto, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Bula>> GetAllAsync(int idEmpresa);
    Task<Entidades.Cadastros.Empresa.Bula> GetByIdAsync(int id, int idEmpresa);
    Task<Entidades.Cadastros.Empresa.Bula> GetByNameAsync(string name, int idEmpresa);
    Task<IEnumerable<(int, int)>> GetDistinctBulaAsync(int idEmpresa, string? nomeProduto);
    Task<IEnumerable<Entidades.Cadastros.Empresa.Bula>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
}
