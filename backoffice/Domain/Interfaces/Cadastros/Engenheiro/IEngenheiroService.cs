using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Engenheiro
{
    public interface IEngenheiroService : IBaseService<Entidades.Cadastros.Engenheiro.Engenheiro>
    {
        Entidades.Cadastros.Engenheiro.Engenheiro BuscarPorId(int? Id);
        List<Entidades.Cadastros.Engenheiro.Engenheiro> ListarTodosEngenheiros();
    }
}
