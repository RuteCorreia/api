using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Cultura
{
    public class CulturaService : BaseService<Entities.Entidades.Cadastros.Cultura.Cultura>, ICulturaService
    {
        public CulturaService(IBaseRepository<Entities.Entidades.Cadastros.Cultura.Cultura> baseRepository) : base(baseRepository)
        {
        }
    }
}
