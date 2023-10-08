using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.CombateIncendio
{
    public interface ICombateIncendioRepository : IBaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio>
    {
        Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio> ListarTodosCombatesIncendio();
    }
}
