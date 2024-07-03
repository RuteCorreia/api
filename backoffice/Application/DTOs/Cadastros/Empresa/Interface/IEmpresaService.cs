using Application.DTOs.Cadastros.Empresa.ViewModel;
using Domain.Enums;

namespace Application.DTOs.Cadastros.Empresa.Interface;

public interface IEmpresaService 
{
    Task<IEnumerable<EmpresaViewModel>> GetAllAsync();

    Task<EmpresaViewModel> GetByIdAsync(int id);

    Task<string> GetLogoByIdAsync(int id);

    Task<(bool, string)> AddAsync(EmpresaViewModel obj);

    Task UpdateAsync(EmpresaViewModel obj);

    Task ChangeStatusAsync(int id, EStatusEmpresa status);

    Task<IEnumerable<EmpresaViewModel>> GetByNameAsync(string name);

    Task DeleteAsync(int id);
}
