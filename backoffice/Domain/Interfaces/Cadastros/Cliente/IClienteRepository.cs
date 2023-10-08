using Domain.Interfaces.Genericos;
using Entities.Entidades.Cadastros.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Cliente
{
    public interface IClienteRepository : IBaseRepository<Entities.Entidades.Cadastros.Cliente.Cliente>
    {
    }
}
