using Domain.Interfaces.Cadastros.Menu;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.Menu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ContextBase _contextBase;
        public MenuRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.Menu.Menu obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public IEnumerable<Domain.Entidades.Cadastros.Menu.Menu> GetAllAsync()
        {
            var entities = _contextBase.Menu.ToList();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.Menu.Menu> GetByIdAsync(int id)
        {
            var obj = await _contextBase.Menu.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.Menu.Menu obj)
        {
            var objeto = await _contextBase.Menu.FindAsync(obj.MenuItemId);
            objeto.Label = obj.Label;
            objeto.Route = obj.Route;
            objeto.Icon = obj.Icon;

            _contextBase.Menu.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
