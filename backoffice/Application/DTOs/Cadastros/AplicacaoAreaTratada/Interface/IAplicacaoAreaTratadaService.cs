using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;

namespace Application.DTOs.Cadastros.AplicacaoAreaTratada.Interface;

public interface IAplicacaoAreaTratadaService 
{
    Task<IEnumerable<AplicacaoAreaTratadaViewModel>> GetAllAsync();

    Task<AplicacaoAreaTratadaViewModel> GetByIdAsync(int id);

    Task AddAsync(AplicacaoAreaTratadaViewModel obj);

    Task UpdateAsync(AplicacaoAreaTratadaViewModel obj);

    Task DeleteAsync(int id);
}
