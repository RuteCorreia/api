using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.PlanoContrato
{
    public class PlanoContratoService : BaseService<PlanoDeContrato>, IPlanoContratoService
    {
        public PlanoContratoService(IBaseRepository<PlanoDeContrato> baseRepository) : base(baseRepository)
        {
        }
    }
}
