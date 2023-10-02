using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AlvoBiologico
{
    public class AlvoBiologicoService : BaseService<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>, IAlvoBiologicoService
    {
        public AlvoBiologicoService(IBaseRepository<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> baseRepository) : base(baseRepository)
        {
        }
    }
}
