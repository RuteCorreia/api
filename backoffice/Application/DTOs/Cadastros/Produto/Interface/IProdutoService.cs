using Application.DTOs.Cadastros.Produto.ViewModel;

namespace Application.DTOs.Cadastros.Produto.Interface;

public interface IProdutoService 
{
    Task<IEnumerable<ProdutoViewModel>> GetAllAsync();

    Task<ProdutoViewModel> GetByIdAsync(int id);

    Task<ProdutoViewModel> GetByNameAsync(string name);

    Task AddAsync(ProdutoViewModel obj, string? idEmpresa);

    Task UpdateAsync(ProdutoViewModel obj);
    Task DeleteAsync(int id);

    Task<IEnumerable<string>> GetClasses();
    Task<IEnumerable<ProdutoNomeViewModel>> GetNomes(string classe);


}
