using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Piloto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Engenheiro
{
    public class EngenheiroRepository : BaseRepository<Entities.Entidades.Cadastros.Engenheiros.Engenheiro>, IEngenheiroRepository
    {
        public EngenheiroRepository(DataContext context) : base(context)
        {
        }
    }
}
