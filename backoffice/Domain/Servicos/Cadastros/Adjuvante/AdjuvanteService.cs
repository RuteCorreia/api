using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Adjuvante
{
    public class AdjuvanteService : BaseService<Entities.Entidades.Cadastros.Adjuvante.Adjuvante>, IAdjuvanteService
    {
        public AdjuvanteService(IBaseRepository<Entities.Entidades.Cadastros.Adjuvante.Adjuvante> baseRepository) : base(baseRepository)
        {
        }
    }
}
