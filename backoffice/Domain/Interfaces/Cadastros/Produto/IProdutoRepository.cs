using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Produto;

public interface IProdutoRepository
{
    Task AddAsync(Entidades.Cadastros.Produto.Produto obj);
    Task UpdateAsync(Entidades.Cadastros.Produto.Produto obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Produto.Produto>> GetAllAsync();
    Task<Entidades.Cadastros.Produto.Produto> GetByIdAsync(int id);
}
