using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Executor
{
    public class ExecutorService : BaseService<Entities.Entidades.Cadastros.Executores.Executor>, IExecutorService
    {
        private readonly IExecutorRepository _executorRepository;

        public ExecutorService(IExecutorRepository executorRepository) : base(executorRepository)
        {
            _executorRepository = executorRepository;
        }

        public Entities.Entidades.Cadastros.Executores.Executor BuscarPorId(int? Id)
        {
            var obj = _executorRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Executores.Executor> ListarTodosExecutores()
        {
            var obj = _executorRepository.ListarTodosExecutores();
            return obj;
        }
    }
}
