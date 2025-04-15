namespace Domain.Interfaces.Cadastros.Produto;

public interface IProdutoRepository
{
    Task AddAsync(Entidades.Cadastros.Produto.Produto obj);
    Task UpdateAsync(Entidades.Cadastros.Produto.Produto obj);
    Task DeleteAsync(int id, int idEmpresa);
    Task<IEnumerable<string>> GetClasses(int idEmpresa);
    Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids, int idEmpresa);
    Task<IEnumerable<Domain.Entidades.Cadastros.Produto.Produto>> GetNomes(string classe, int idEmpresa);
    Task<Domain.Entidades.Cadastros.Produto.Produto> GetByNameAsync(string nome, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Produto.Produto>> GetAllAsync(string? nomeProduto, int idEmpresa);
    Task<Entidades.Cadastros.Produto.Produto> GetByIdAsync(int? id, int idEmpresa);
    Task<IEnumerable<Entidades.Cadastros.Produto.Produto>> GetByDateAsync(int idEmpresa, DateTime dataUltimaSincronizacao);
}
