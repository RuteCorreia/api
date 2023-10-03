using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.CombateIncendioDecolagemPouso
{
    public class CombateIncendioDecolagemPousoRepository : BaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>, ICombateIncendioDecolagemPousoRepository
    {
        public CombateIncendioDecolagemPousoRepository(DataContext context) : base(context)
        {
        }
    }
}
