using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Executor;

namespace Application.Application.Servicos.Cadastros.Executor
{
    public class ExecutorService : BaseService<Domain.Entidades.Cadastros.Executor.Executor>, IExecutorService
    {
        private readonly IExecutorRepository _executorRepository;

        public ExecutorService(IExecutorRepository executorRepository) : base(executorRepository)
        {
            _executorRepository = executorRepository;
        }

        public Domain.Entidades.Cadastros.Executor.Executor BuscarPorId(int? Id)
        {
            var obj = _executorRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Executor.Executor> ListarTodosExecutores()
        {
            var obj = _executorRepository.ListarTodosExecutores();
            return obj;
        }
    }
}
