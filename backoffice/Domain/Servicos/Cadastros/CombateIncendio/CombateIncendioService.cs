using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.CombateIncendio
{
    public class CombateIncendioService : BaseService<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio>, ICombateIncendioService
    {
        public CombateIncendioService(IBaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio> baseRepository) : base(baseRepository)
        {
        }
    }
}
