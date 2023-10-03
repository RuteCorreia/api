using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.CombateIncendio
{
    public class CombateIncendioRepository : BaseRepository<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendio>, ICombateIncendioRepository
    {
        public CombateIncendioRepository(DataContext context) : base(context)
        {
        }
    }
}
