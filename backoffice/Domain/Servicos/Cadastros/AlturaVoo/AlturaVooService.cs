using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AlturaVoo
{
    public class AlturaVooService : BaseService<Entities.Entidades.Cadastros.Altura_Voo.AlturaVoo>, IAlturaVooService
    {
        public AlturaVooService(IBaseRepository<Entities.Entidades.Cadastros.Altura_Voo.AlturaVoo> baseRepository) : base(baseRepository)
        {
        }
    }
}
