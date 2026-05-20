using Application.DTOs.Cadastros.Classe.ViewModel;

namespace Application.DTOs.Cadastros.Classe.Interface
{
    public interface IClasseService
    {
        Task<IEnumerable<ClasseViewModel>> GetByTipoServicoAsync(int idTipoDeServico);
        Task<ClasseViewModel?> GetByIdAsync(int id);
        Task<ClasseViewModel> AddAsync(ClasseCreateViewModel obj);
        Task<string?> UpdateAsync(int id, ClasseUpdateViewModel obj);
        Task<string?> DeleteAsync(int id);
    }
}
