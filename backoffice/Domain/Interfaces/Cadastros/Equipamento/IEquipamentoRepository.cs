using Domain.Interfaces.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Equipamento
{
    public interface IEquipamentoRepository : IBaseRepository<Entities.Entidades.Cadastros.Equipamento.Equipamento>
    {
    }
}
