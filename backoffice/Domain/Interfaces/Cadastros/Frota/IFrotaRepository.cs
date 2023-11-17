using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Frota
{
    public interface IFrotaRepository : IBaseRepository<Domain.Entidades.Cadastros.Frota.Frota>
    {
        Domain.Entidades.Cadastros.Frota.Frota BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.Frota.Frota> ListarFrotas();
    }
}
