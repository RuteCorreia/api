using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso
{
    public interface ICombateIncendioDecolagemPousoService : IBaseService<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>
    {
        Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> ListarTodosCombatesIncendioDecolagemPouso();
    }
}
