using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Aeronave
{
    public class AeronaveService : BaseService<Entities.Entidades.Cadastros.Aeronaves.Aeronave>, IAeronaveService
    {
        public AeronaveService(IBaseRepository<Entities.Entidades.Cadastros.Aeronaves.Aeronave> baseRepository) : base(baseRepository)
        {
        }
    }
}
