using Domain.Interfaces;
using Domain.Interfaces.Empresa;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Empresa
{
    public class EmpresaService : BaseService<Entities.Entidades.Cadastros.Empresa.Empresa>, IEmpresaService
    {
        public EmpresaService(IBaseRepository<Entities.Entidades.Cadastros.Empresa.Empresa> baseRepository) : base(baseRepository)
        {
        }
    }
}
