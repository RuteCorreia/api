using Application.DTOs.Cadastros.Bula.ViewModel;

namespace Application.DTOs.Cadastros.Bula.Interface;

public interface IBulaService 
{
    Task<IEnumerable<BulaAppViewModel>> GetAllAsync(string? idEmpresa);

    Task<BulaViewModel> GetByIdAsync(int id, string? idEmpresa);
    Task<BulaViewModel> GetByName(string name, string? idEmpresa);
    Task<IEnumerable<(int, int)>> GetDistinctBulaAsync(string? idEmpresa,string? nomeProduto);
    Task<BulaViewModel> GetByIdProdutoAsync(int idProduto, string? idEmpresa);
    Task RemoveRecomendacaoAsync(int idBula, string? idEmpresa);
    Task RemoveBulaAsync(int idProduto, string? idEmpresa);

    Task AddAsync(BulaViewModel obj, string? idEmpresa);

    Task UpdateAsync(BulaViewModel obj);

    Task DeleteAsync(int id, string? idEmpresa);
}
