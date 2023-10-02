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
        public ExecutorService(IBaseRepository<Entities.Entidades.Cadastros.Executores.Executor> baseRepository) : base(baseRepository)
        {
        }
    }
}
