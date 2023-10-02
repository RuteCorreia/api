using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Combustivel
{
    public class CombustivelService : BaseService<Entities.Entidades.Cadastros.Combustivel.Combustivel>, ICombustivelService
    {
        public CombustivelService(IBaseRepository<Entities.Entidades.Cadastros.Combustivel.Combustivel> baseRepository) : base(baseRepository)
        {
        }
    }
}
