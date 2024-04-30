using Application.DTOs.Cadastros.Bula.ViewModel;
using Application.DTOs.Cadastros.Menu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Menu.Interface
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuViewModel>> GetAllAsync(string? idEmpresa);

        Task<MenuViewModel> GetByIdAsync(int id);

        Task AddAsync(MenuViewModel obj);

        Task UpdateAsync(MenuViewModel obj);

        Task DeleteAsync(int id);
    }
}
