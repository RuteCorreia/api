using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.CombateIncendioDecolagemPouso
{
    public class CombateIncendioDecolagemPousoService : BaseService<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>, ICombateIncendioDecolagemPousoService
    {
        public CombateIncendioDecolagemPousoService(IBaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> baseRepository) : base(baseRepository)
        {
        }
    }
}
