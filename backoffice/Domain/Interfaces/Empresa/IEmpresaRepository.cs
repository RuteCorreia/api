using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Empresa
{
    public interface IEmpresaRepository : IBaseRepository<Entities.Entidades.Cadastros.Empresa.Empresa>
    {
    }
}
