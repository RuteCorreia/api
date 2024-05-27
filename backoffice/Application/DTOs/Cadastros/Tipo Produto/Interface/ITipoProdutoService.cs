using Application.DTOs.Cadastros.Tipo_Produto.ViewModel;

namespace Application.DTOs.Cadastros.Tipo_Produto.Interface;

public interface ITipoProdutoService 
{
    Task<IEnumerable<TipoProdutoViewModel>> GetAllAsync();

    Task<TipoProdutoViewModel> GetByIdAsync(int id);

    Task AddAsync(TipoProdutoViewModel obj);

    Task UpdateAsync(TipoProdutoViewModel obj);

    Task DeleteAsync(int id);
}
