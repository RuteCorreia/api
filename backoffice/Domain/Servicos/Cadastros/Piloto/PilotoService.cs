using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Piloto
{
    public class PilotoService : BaseService<Entities.Entidades.Cadastros.Pilotos.Piloto>, IPilotoService
    {
        public PilotoService(IBaseRepository<Entities.Entidades.Cadastros.Pilotos.Piloto> baseRepository) : base(baseRepository)
        {
        }
    }
}
