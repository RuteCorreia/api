using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Bula
{
    public class BulaService : BaseService<Entities.Entidades.Cadastros.Empresa.Bula>, IBulaService
    {
        public BulaService(IBaseRepository<Entities.Entidades.Cadastros.Empresa.Bula> baseRepository) : base(baseRepository)
        {
        }
    }
}
