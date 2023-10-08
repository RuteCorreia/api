using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Bula
{
    public interface IBulaService : IBaseService<Entities.Entidades.Cadastros.Empresa.Bula>
    {
        Entities.Entidades.Cadastros.Empresa.Bula BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Empresa.Bula> ListarTodasBulas();
    }
}
