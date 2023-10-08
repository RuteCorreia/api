using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Aeronave
{
    public interface IAeronaveService : IBaseService<Entities.Entidades.Cadastros.Aeronaves.Aeronave>
    {
        Entities.Entidades.Cadastros.Aeronaves.Aeronave BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Aeronaves.Aeronave> ListarTodasAeronaves();
    }
}
