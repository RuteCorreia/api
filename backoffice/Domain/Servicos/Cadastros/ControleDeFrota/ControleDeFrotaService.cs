using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.ControleDeFrota
{
    public class ControleDeFrotaService : BaseService<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>, IControleDeFrotaService
    {
        public ControleDeFrotaService(IBaseRepository<Entities.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> baseRepository) : base(baseRepository)
        {
        }
    }
}
