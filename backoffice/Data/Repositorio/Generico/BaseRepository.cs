using Data.Context;
using Domain.Interfaces.Genericos;
using Entities.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Generico
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Inserir(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            _context.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            _context.Set<TEntity>().Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public IList<TEntity> ListarTodos() =>
            _context.Set<TEntity>().ToList();

        public TEntity BuscarPorId(int id) =>
            _context.Set<TEntity>().Find(id);
    }
}
