using Application.DTOs.Cadastros.Engenheiro.ViewModel;

namespace Application.DTOs.Cadastros.Engenheiro.Interface;

public interface IEngenheiroService 
{
    Task<IEnumerable<EngenheiroViewModel>> GetAllAsync();

    Task<EngenheiroViewModel> GetByIdAsync(int id);

    Task AddAsync(EngenheiroViewModel obj);

    Task UpdateAsync(EngenheiroViewModel obj);

    Task DeleteAsync(int id);
}
