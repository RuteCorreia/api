using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Piloto
{
    public interface IPilotoService : IBaseService<Entities.Entidades.Cadastros.Pilotos.Piloto>
    {
        Entities.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos();
    }
}
