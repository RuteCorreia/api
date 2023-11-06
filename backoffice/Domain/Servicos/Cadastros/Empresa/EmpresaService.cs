using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Empresa
{
    public class EmpresaService : BaseService<Domain.Entidades.Cadastros.Empresa.Empresa>, IEmpresaService
    {
        public EmpresaService(IBaseRepository<Domain.Entidades.Cadastros.Empresa.Empresa> baseRepository) : base(baseRepository)
        {
        }
    }
}
