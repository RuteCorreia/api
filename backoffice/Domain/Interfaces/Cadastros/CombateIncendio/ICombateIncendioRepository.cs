using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.CombateIncendio
{
    public interface ICombateIncendioRepository : IBaseRepository<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>
    {
        Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> ListarTodosCombatesIncendio();
    }
}
