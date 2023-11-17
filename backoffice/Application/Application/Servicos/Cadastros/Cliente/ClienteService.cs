using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Genericos;

namespace Application.Application.Servicos.Cadastros.Cliente
{
    public class ClienteService : BaseService<Domain.Entidades.Cadastros.Cliente.Cliente>, IClienteService
    {
        public ClienteService(IBaseRepository<Domain.Entidades.Cadastros.Cliente.Cliente> baseRepository) : base(baseRepository)
        {
        }
    }
}
