using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Cliente
{
    public class ClienteService : BaseService<Entities.Entidades.Cadastros.Cliente.Cliente>, IClienteService
    {
        public ClienteService(IBaseRepository<Entities.Entidades.Cadastros.Cliente.Cliente> baseRepository) : base(baseRepository)
        {
        }
    }
}
