using Application.DTOs.Cadastros.Cidades.ViewModel;

namespace Application.DTOs.Cadastros.Cidades.Interface;

public interface ICidadeService
{
    Task<IEnumerable<CidadeViewModel>> GetAllAsync();

    Task<CidadeViewModel> GetByIdAsync(int id);

    Task AddAsync(CidadeViewModel obj);

    Task UpdateAsync(CidadeViewModel obj);

    Task DeleteAsync(int id);
}
