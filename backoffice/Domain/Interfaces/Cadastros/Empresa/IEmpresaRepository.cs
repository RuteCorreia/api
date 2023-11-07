using Domain.Interfaces.Genericos;
using Domain.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Empresa
{
    public interface IEmpresaRepository : IBaseRepository<Domain.Entidades.Cadastros.Empresa.Empresa>
    {
    }
}
