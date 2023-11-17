using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Engenheiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Cultura
{
    public class CulturaRepository : BaseRepository<Entities.Entidades.Cadastros.Cultura.Cultura>, ICulturaRepository
    {
        public CulturaRepository(DataContext context) : base(context)
        {
        }
    }
}
