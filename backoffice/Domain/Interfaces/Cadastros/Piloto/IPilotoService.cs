using Domain.Entidades.Cadastros.Empresa;
using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Piloto
{
    public interface IPilotoService : IBaseService<Domain.Entidades.Cadastros.Pilotos.Piloto>
    {
        Domain.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos();
    }
}
