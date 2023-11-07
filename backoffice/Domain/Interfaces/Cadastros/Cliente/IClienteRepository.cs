using Domain.Interfaces.Genericos;
using Domain.Entidades.Cadastros.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Cliente
{
    public interface IClienteRepository : IBaseRepository<Domain.Entidades.Cadastros.Cliente.Cliente>
    {
    }
}
