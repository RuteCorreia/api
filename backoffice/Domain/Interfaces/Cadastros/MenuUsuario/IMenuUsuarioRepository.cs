using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.MenuUsuario
{
    public interface IMenuUsuarioRepository
    {
        Task AddAsync(Entidades.Cadastros.MenuUsuario.MenuUsuario obj);
        Task UpdateAsync(Entidades.Cadastros.MenuUsuario.MenuUsuario obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.MenuUsuario.MenuUsuario>> GetAllAsync();
        Task<Entidades.Cadastros.MenuUsuario.MenuUsuario> GetByIdAsync(string id);
    }
}
