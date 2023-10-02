using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Pista
{
    public class PistaService : BaseService<Entities.Entidades.Cadastros.Pistas.Pista>, IPistaService
    {
        public PistaService(IBaseRepository<Entities.Entidades.Cadastros.Pistas.Pista> baseRepository) : base(baseRepository)
        {
        }
    }
}
