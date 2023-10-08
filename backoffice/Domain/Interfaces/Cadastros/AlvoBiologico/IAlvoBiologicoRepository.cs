using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.AlvoBiologico
{
    public interface IAlvoBiologicoRepository : IBaseRepository<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>
    {
        Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> ListarTodosAlvosBiologicos();
    }
}
