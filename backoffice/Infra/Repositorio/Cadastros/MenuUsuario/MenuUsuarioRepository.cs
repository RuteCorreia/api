using Domain.Interfaces.Cadastros.MenuUsuario;
using Helpers;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Cadastros.MenuUsuario
{
    public class MenuUsuarioRepository : IMenuUsuarioRepository
    {
        private readonly ContextBase _contextBase;

        public MenuUsuarioRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task AddAsync(Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            //var entityToRemove = await GetByIdAsync(id);
            //if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            //{
            //    _contextBase.Remove(entityToRemove);
            //    await _contextBase.SaveChangesAsync();
            //}
        }

        public async Task<IEnumerable<Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario>> GetAllAsync()
        {
            var entities = await _contextBase.MenuUsuario.ToListAsync();
            return entities;
        }

        public async Task<Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario> GetByIdAsync(string id)
        {
            var obj = _contextBase.MenuUsuario.Where(x => x.Usuario.Id == id).FirstOrDefault();
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.MenuUsuario.MenuUsuario obj)
        {
            var objeto = await _contextBase.MenuUsuario.FindAsync(obj.Id);
            objeto.IdSubMenu = obj.IdSubMenu;
            objeto.IdUsuario = obj.IdUsuario;

            _contextBase.MenuUsuario.Update(objeto);
            await _contextBase.SaveChangesAsync();
        }
    }
}
