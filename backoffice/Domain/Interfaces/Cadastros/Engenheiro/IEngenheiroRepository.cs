using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Engenheiro
{
    public interface IEngenheiroRepository : IBaseRepository<Entities.Entidades.Cadastros.Engenheiros.Engenheiro>
    {
        Entities.Entidades.Cadastros.Engenheiros.Engenheiro BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Engenheiros.Engenheiro> ListarTodosEngenheiros();
    }
}
