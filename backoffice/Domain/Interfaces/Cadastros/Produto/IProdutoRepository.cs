using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Produto;

public interface IProdutoRepository
{
    Task AddAsync(Entidades.Cadastros.Produto.Produto obj);
    Task UpdateAsync(Entidades.Cadastros.Produto.Produto obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<string>> GetClasses(int idEmpresa);
    Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids);
    Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe, int idEmpresa);
    Task<Domain.Entidades.Cadastros.Produto.Produto> GetByNameAsync(string nome, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Produto.Produto>> GetAllAsync(string? nomeProduto, int idEmpresa);
    Task<Entidades.Cadastros.Produto.Produto> GetByIdAsync(int? id);
}
