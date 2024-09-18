using Application.DTOs.Cadastros.Bula.ViewModel;

namespace Application.DTOs.Cadastros.Bula.Interface;

public interface IBulaService 
{
    Task<IEnumerable<BulaViewModel>> GetAllAsync(string? idEmpresa);

    Task<BulaViewModel> GetByIdAsync(int id);
    Task<BulaViewModel> GetByName(string name, string? idEmpresa);
    Task<IEnumerable<int>> GetDistinctBulaAsync(string? idEmpresa,string? nomeProduto);
    Task<BulaViewModel> GetByIdProdutoAsync(int idProduto);
    Task RemoveRecomendacaoAsync(int idBula);
    Task RemoveBulaAsync(int idProduto, string? idEmpresa);

    Task AddAsync(BulaViewModel obj, string? idEmpresa);

    Task UpdateAsync(BulaViewModel obj);

    Task DeleteAsync(int id);
}
