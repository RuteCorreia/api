using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Empresa
{
    public class EmpresaRepository : BaseRepository<Entities.Entidades.Cadastros.Empresa.Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(DataContext context) : base(context)
        {
        }
    }
}
