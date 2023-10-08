using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.ControleDeFrota
{
    public interface IControleDeFrotaService : IBaseService<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>
    {
        Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota BuscarPorId(int? Id);
        List<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> ListarTodosControlesDeFrota();
    }
}
