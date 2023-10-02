using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Executor
{
    public class ExecutorRepository : BaseRepository<Entities.Entidades.Cadastros.Executores.Executor>, IExecutorRepository
    {
        public ExecutorRepository(DataContext context) : base(context)
        {
        }
    }
}
