using Domain.Interfaces.Genericos;
using Entities.Entidades.Cadastros.Pilotos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Piloto
{
    public interface IPilotoRepository : IBaseRepository<Entities.Entidades.Cadastros.Pilotos.Piloto>
    {
        Entities.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos();
    }
}
