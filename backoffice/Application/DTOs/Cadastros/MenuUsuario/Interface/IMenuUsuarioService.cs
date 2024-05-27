using Application.DTOs.Cadastros.Menu.ViewModel;
using Application.DTOs.Cadastros.MenuUsuario.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.MenuUsuario.Interface
{
    public interface IMenuUsuarioService
    {
        Task<IEnumerable<MenuUsuarioViewModel>> GetAllAsync();

        Task<MenuUsuarioViewModel> GetByIdAsync(string id);

        Task AddAsync(MenuUsuarioViewModel obj);

        Task UpdateAsync(MenuUsuarioViewModel obj);

        Task DeleteAsync(int id);
    }
}
