using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Cliente
{
    public class ClienteRepository : BaseRepository<Entities.Entidades.Cadastros.Cliente.Cliente>, IClienteRepository
    {
        public ClienteRepository(DataContext context) : base(context)
        {
        }
    }
}
