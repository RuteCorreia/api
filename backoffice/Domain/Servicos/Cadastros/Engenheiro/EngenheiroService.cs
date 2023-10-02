using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Engenheiro
{
    public class EngenheiroService : BaseService<Entities.Entidades.Cadastros.Engenheiros.Engenheiro>, IEngenheiroService
    {
        public EngenheiroService(IBaseRepository<Entities.Entidades.Cadastros.Engenheiros.Engenheiro> baseRepository) : base(baseRepository)
        {
        }
    }
}
