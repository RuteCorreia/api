using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Produto;

public interface IProdutoRepository
{
    Task AddAsync(Entidades.Cadastros.Produto.Produto obj);
    Task UpdateAsync(Entidades.Cadastros.Produto.Produto obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<string>> GetClasses();
    Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe);
    Task<IEnumerable<Entidades.Cadastros.Produto.Produto>> GetAllAsync();
    Task<Entidades.Cadastros.Produto.Produto> GetByIdAsync(int id);
}
