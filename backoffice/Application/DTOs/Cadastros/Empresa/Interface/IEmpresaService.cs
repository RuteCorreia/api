using Application.DTOs.Cadastros.Empresa.ViewModel;

namespace Application.DTOs.Cadastros.Empresa.Interface;

public interface IEmpresaService 
{
    Task<IEnumerable<EmpresaViewModel>> GetAllAsync();

    Task<EmpresaViewModel> GetByIdAsync(int id);

    Task AddAsync(EmpresaViewModel obj);

    Task UpdateAsync(EmpresaViewModel obj);

    Task DeleteAsync(int id);
}
