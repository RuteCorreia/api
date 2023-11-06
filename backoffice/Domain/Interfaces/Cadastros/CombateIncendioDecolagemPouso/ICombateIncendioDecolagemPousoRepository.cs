using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso
{
    public interface ICombateIncendioDecolagemPousoRepository : IBaseRepository<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>
    {
        Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> ListarTodosCombatesIncendioDecolagemPouso();
    }
}
