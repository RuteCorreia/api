using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Aeronave
{
    public interface IAeronaveRepository : IBaseRepository<Entities.Entidades.Cadastros.Aeronaves.Aeronave>
    {
    }
}
