using Application.DTOs.Cadastros.Produto.ViewModel;
using Domain.Entidades.Cadastros.Empresa;

namespace Application.DTOs.Cadastros.Produto.Interface;
public interface IProdutoService 
{
    Task<IEnumerable<ProdutoViewModel>> GetAllAsync(string? nomeProduto, string? idEmpresa);
    Task<IEnumerable<ProdutoViewModel>> GetAllAppAsync(string? nomeProduto, string? idEmpresa);

    Task<ProdutoViewModel> GetByIdAppAsync(int id);

    Task<ProdutoViewModel> GetByIdAsync(int id);

    Task<ProdutoViewModel> GetByNameAsync(string name);

    Task AddAsync(ProdutoViewModel obj, string? idEmpresa);
    Task<IEnumerable<string>> GetNomesByIdsAsync(List<int> ids);

    Task UpdateAsync(ProdutoViewModel obj);
    Task DeleteAsync(int id);

    Task<IEnumerable<string>> GetClasses();
    Task<IEnumerable<ProdutoNomeViewModel>> GetNomes(string classe);


}
