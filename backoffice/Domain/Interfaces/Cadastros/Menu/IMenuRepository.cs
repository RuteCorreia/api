using Domain.Entidades.Cadastros.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Menu
{
    public interface IMenuRepository
    {
        Task AddAsync(Entidades.Cadastros.Menu.Menu obj);
        Task UpdateAsync(Entidades.Cadastros.Menu.Menu obj);
        Task DeleteAsync(int id);
        IEnumerable<Entidades.Cadastros.Menu.Menu> GetAllAsync();
        Task<Entidades.Cadastros.Menu.Menu> GetByIdAsync(int id);
    }
}
