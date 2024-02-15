using Domain.Interfaces.Cadastros.SubMenu;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.SubMenu
{
    public class SubMenuRepository : ISubMenuRepository
    {
        private readonly ContextBase _contextBase;

        public SubMenuRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task AddAsync(Domain.Entidades.Cadastros.SubMenu.SubMenu obj)
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

        public async Task<IEnumerable<Domain.Entidades.Cadastros.SubMenu.SubMenu>> GetAllAsync()
        {
            var entities = await _contextBase.SubMenu.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.SubMenu.SubMenu> GetByIdAsync(int id)
        {
            var obj = await _contextBase.SubMenu.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.SubMenu.SubMenu obj)
        {
            var objeto = await _contextBase.SubMenu.FindAsync(obj.SubMenuId);
            objeto.Label = obj.Label;
            objeto.Route = obj.Route;
            objeto.Icon = obj.Icon;

            _contextBase.SubMenu.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
