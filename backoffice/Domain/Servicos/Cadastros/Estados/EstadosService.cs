using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Estados;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Estados
{
    public class EstadosService : BaseService<Entities.Entidades.Cadastros.Estados.Estados>, IEstadosService
    {
        public EstadosService(IBaseRepository<Entities.Entidades.Cadastros.Estados.Estados> baseRepository) : base(baseRepository)
        {
        }
    }
}
