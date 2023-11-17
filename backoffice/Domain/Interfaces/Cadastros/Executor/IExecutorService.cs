using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Executor
{
    public interface IExecutorService : IBaseService<Entidades.Cadastros.Executor.Executor>
    {
        Entidades.Cadastros.Executor.Executor BuscarPorId(int? Id);
        List<Entidades.Cadastros.Executor.Executor> ListarTodosExecutores();
    }
}
