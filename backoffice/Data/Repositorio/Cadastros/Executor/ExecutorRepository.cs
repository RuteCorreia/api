using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Executor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Executor
{
    public class ExecutorRepository : BaseRepository<Entities.Entidades.Cadastros.Executores.Executor>, IExecutorRepository
    {
        protected readonly DataContext _context;

        public ExecutorRepository(DataContext context) : base(context)
        {
            _context = context;

        }

        public Entities.Entidades.Cadastros.Executores.Executor BuscarPorId(int? Id)
        {
            var obj = _context.Executor.Where(x => x.IdExecutor == Id).Include("Empresa").FirstOrDefault();
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Executores.Executor> ListarTodosExecutores()
        {
            var obj = _context.Executor.Include("Empresa").ToList();
            return obj;
        }
    }
}
