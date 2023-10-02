using Data.Context;
using Data.Repositorio.Generico;
using Domain.Interfaces.Cadastros.Piloto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio.Cadastros.Piloto
{
    public class PilotoRepository : BaseRepository<Entities.Entidades.Cadastros.Pilotos.Piloto>, IPilotoRepository
    {
        public PilotoRepository(DataContext context) : base(context)
        {
        }
    }
}
