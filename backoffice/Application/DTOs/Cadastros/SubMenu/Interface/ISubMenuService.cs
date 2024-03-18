using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.SubMenu.Interface
{
    public interface ISubMenuService
    {
        IEnumerable<SubMenuViewModel> GetAllAsync();

        Task<SubMenuViewModel> GetByIdAsync(int id);

        Task AddAsync(SubMenuViewModel obj);

        Task UpdateAsync(SubMenuViewModel obj);

        Task DeleteAsync(int id);
    }
}
