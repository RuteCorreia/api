using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.TipoProduto;

public interface ITipoProdutoRepository
{
    Task AddAsync(Entidades.Cadastros.Tipo_Produto.TipoProduto obj);
    Task UpdateAsync(Entidades.Cadastros.Tipo_Produto.TipoProduto obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Tipo_Produto.TipoProduto>> GetAllAsync();
    Task<Entidades.Cadastros.Tipo_Produto.TipoProduto> GetByIdAsync(int id);
    Task<IEnumerable<Entidades.Cadastros.Tipo_Produto.TipoProduto>> GetByDateAsync(DateTime dataUltimaSincronizacao);
}
