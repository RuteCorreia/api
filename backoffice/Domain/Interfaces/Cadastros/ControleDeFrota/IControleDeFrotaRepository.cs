using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.ControleDeFrota
{
    public interface IControleDeFrotaRepository : IBaseRepository<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>
    {
        Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota BuscarPorId(int? Id);
        List<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> ListarTodosControlesDeFrota();
    }
}
