namespace Domain.Interfaces.Cadastros.ProdutoAplicado;

public interface IProdutoAplicadoRepository
{
    Task<int> AddAsync(Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado obj);
    Task UpdateAsync(Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado>> GetAllByIdRelatorioAplicacaoAsync(int relatorioAplicacaoId);
    Task<Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado> GetByIdAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.ProdutoAplicado.ProdutoAplicado>> GetAllAsync();
}
