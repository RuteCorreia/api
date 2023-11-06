using Domain.Interfaces.Cadastros.Veiculante;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Veiculante
{
    public class VeiculanteService : BaseService<Domain.Entidades.Cadastros.Veiculante.Veiculante>, IVeiculanteService
    {
        public VeiculanteService(IBaseRepository<Domain.Entidades.Cadastros.Veiculante.Veiculante> baseRepository) : base(baseRepository)
        {
        }
    }
}
