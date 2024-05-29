using Application.DTOs.Cadastros.SubMenu.ViewModel;

namespace Application.DTOs.Cadastros.SubMenu.Interface;

public interface ISubMenuService
{
    Task<IEnumerable<SubMenuViewModel>> GetAllAsync();

    Task<SubMenuViewModel> GetByIdAsync(int id);

    Task AddAsync(SubMenuViewModel obj);

    Task UpdateAsync(SubMenuViewModel obj);

    Task DeleteAsync(int id);
}
