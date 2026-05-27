using Application.DTOs.Cadastros.Classe.ViewModel;

namespace Application.DTOs.Cadastros.Classe.Interface
{
    public interface IClasseService
    {
        Task<IEnumerable<ClasseViewModel>> GetAllAsync();
        Task<IEnumerable<ClasseViewModel>> GetByTipoServicoAsync(int idTipoDeServico, string? idEmpresa);
        Task<ClasseViewModel?> GetByIdAsync(int id);
        Task<ClasseViewModel> AddAsync(ClasseCreateViewModel obj);
        Task<string?> UpdateAsync(int id, ClasseUpdateViewModel obj);
        Task<string?> DeleteAsync(int id);
    }
}
