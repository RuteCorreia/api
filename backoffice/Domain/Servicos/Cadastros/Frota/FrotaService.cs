using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Frota
{
    public class FrotaService : BaseService<Entities.Entidades.Cadastros.Frota.Frota>, IFrotaService
    {
        public FrotaService(IBaseRepository<Entities.Entidades.Cadastros.Frota.Frota> baseRepository) : base(baseRepository)
        {
        }
    }
}
